using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks.Triggers;

/// <summary>
/// 敵オブジェクトに付ける行動用スクリプト
/// プレイヤーとの距離がX以下になると画面←弾を撃つ
/// </summary>
public class Enemy : MonoBehaviour, IHit, IMovable
{
    Player _player;
    [SerializeField] float _detectionDistance = 4;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _hp = 5;
    [SerializeField] int _damage = 10;
    [SerializeField] EnemyType _type = EnemyType.Shoot;
    [SerializeField] float _moveSpeed = 1;

    bool _isHit = false;
    bool _active = false;
    float _laserHitStopTime = 0.25f;
    float _bulletHitStopTime = 0.08f;

    Vector3 _dir = Vector3.zero;
    float _shootDur = 1;
    BoolReactiveProperty _isShoot = new BoolReactiveProperty();
    CancellationTokenSource _cancellationTokenSource;
    List<GameObject> _playerObj = new List<GameObject>();

    int _knockBackFrame = 8;
    int _knockBackPow = 1;

    enum EnemyType
    {
        Shoot,
        Move,
    }

    void Awake()
    {
        ServiceLocator.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_type == EnemyType.Shoot)
        {
            _isShoot.Where(x => x == true).Subscribe(_ => Shoot()).AddTo(this);
            _cancellationTokenSource = new CancellationTokenSource();
        }
        _player = ServiceLocator.ResolveAll<Player>()[0];
        _playerObj.Add(ServiceLocator.ResolveAll<Player>()[0].gameObject);
    }

    private void Update()
    {
        if (_isHit) return;
        if (!_player) return;

        if (this.transform.position.x - _player.transform.position.x <= _detectionDistance)
        {
            _active = true;
        }
        
        if (_active)
        {
            if (_type == EnemyType.Shoot) _isShoot.Value = true;

            if (_type == EnemyType.Move) MoveAttack();
        }
    }

    void MoveAttack()
    {
        if (_dir == Vector3.zero) _dir = _player.transform.position - this.transform.position;
        

        var target = MyCollision.CircleCollision(this.gameObject, _playerObj);

        foreach (var obj in target)
        {
            obj.GetComponent<IHit>().Hit(_damage, this.transform.position);
        }

        transform.position += _dir * _moveSpeed * Time.deltaTime;
    }

    async UniTaskVoid Shoot()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            var bullet = Instantiate(_bullet, transform.position, transform.rotation);
            var targetList = new List<GameObject>() { _player.gameObject };
            var dir = (_player.transform.position - transform.position).normalized;
            bullet.GetComponent<Bullet>().InitializedBullet(targetList, dir);
            await UniTask.Delay(TimeSpan.FromSeconds(_shootDur), cancellationToken: _cancellationTokenSource.Token);
        }
    }

    public void Hit(int damage, Vector3 dir)
    {
        _hp -= damage;

        StartCoroutine(nameof(KnockBack), dir);

        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator KnockBack(Vector3 dir)
    {
        for (int i = 0; i < _knockBackFrame; i++)
        {
            this.transform.position += (transform.position - dir).normalized * _knockBackPow * Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister(this);

        if (_cancellationTokenSource != null) _cancellationTokenSource.Cancel();
    }

    public void Stop(float time)
    {
        Debug.Log("Stop");
        _isHit = true;
        StartCoroutine(nameof(HitStop), time);
    }

    IEnumerator HitStop(float time)
    {
        yield return new WaitForSeconds(time);
        _isHit = false;
    }
}
