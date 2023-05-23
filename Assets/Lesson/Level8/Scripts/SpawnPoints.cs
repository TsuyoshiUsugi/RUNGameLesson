using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵の生成地点のスクリプト
/// 指定された秒数おきに、生成した敵がまだ生きているかを確認し、死んでいたらスポーン
/// </summary>
public class SpawnPoints : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] float _spawnDur = 5;
    GameObject _cashEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(_spawnDur))
            .Subscribe(_ => Spawn()).AddTo(this);
    }

    void Spawn()
    {
        if (_cashEnemy == null)
        {
            var obj = Instantiate(_enemy, transform.position, Quaternion.identity);
            _cashEnemy = obj;
        }
    }
}
