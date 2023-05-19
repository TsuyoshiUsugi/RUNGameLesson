using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

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
    float _shootDur = 1;
    BoolReactiveProperty _isShoot = new BoolReactiveProperty();
    CancellationTokenSource _cancellationTokenSource;

    void Awake()
    {
        ServiceLoacator.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _isShoot.Where(x => x == true).Subscribe(_ => Shoot()).AddTo(this);
        _cancellationTokenSource = new CancellationTokenSource();
        _player = ServiceLoacator.ResolveAll<Player>()[0];
    }

    private void Update()
    {
        if (this.transform.position.x - _player.transform.position.x <= _detectionDistance)
        {
            _isShoot.Value = true;
        }
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

    public void Hit(int damage)
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
        _cancellationTokenSource.Cancel();
    }
}
