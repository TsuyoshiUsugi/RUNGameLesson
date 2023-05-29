using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 敵オブジェクトに付ける行動用スクリプト
/// プレイヤーとの距離がX以下になると画面←弾を撃[つ
/// </summary>
public class Enemy : MonoBehaviour, IHit
{
    Player _player;
    [SerializeField] float _detectionDistance = 4;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _hp = 5;
    [SerializeField] int _damage = 10;
    [SerializeField] EnemyType _type = EnemyType.Shoot;
    [SerializeField] float _moveSpeed = 1;

    Vector3 _dir = Vector3.zero;
    float _shootDur = 1;
    BoolReactiveProperty _isShoot = new BoolReactiveProperty();
    CancellationTokenSource _cancellationTokenSource;
    List<GameObject> _playerObj = new List<GameObject>();

    enum EnemyType
    {
        Shoot,
        Move,
    }

    void Awake()
    {
        ServiceLoacator.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_type == EnemyType.Shoot)
        {
            _isShoot.Where(x => x == true).Subscribe(_ => Shoot()).AddTo(this);
            _cancellationTokenSource = new CancellationTokenSource();
        }
        _player = ServiceLoacator.ResolveAll<Player>()[0];
        _playerObj.Add(ServiceLoacator.ResolveAll<Player>()[0].gameObject);
    }

    private void Update()
    {

        if (this.transform.position.x - _player.transform.position.x <= _detectionDistance)
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
            Debug.Log(obj.name);
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
            bullet.GetComponent<Bullet>().InitializedBullet(targetList, Vector3.left);
            await UniTask.Delay(TimeSpan.FromSeconds(_shootDur), cancellationToken: _cancellationTokenSource.Token);
        }
    }

    public void Hit(int damage, Vector3 dir)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        ServiceLoacator.Unregister(this);

        if (_cancellationTokenSource != null) _cancellationTokenSource.Cancel();
    }
}
