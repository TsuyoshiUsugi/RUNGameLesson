using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが一定まで近づくと指定した地点に敵を一斉に生成する
/// 一度だけ起動
/// </summary>
public class SpwanPoints2 : MonoBehaviour
{
    [SerializeField] GameObject _spawnEnemy;
    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] float _activeDir = 2;
    private readonly int _leastSpawnPointNum = 2;
    Player _player;

    private void Start()
    {
        if( _spawnPoints.Count < _leastSpawnPointNum)
        {
            Debug.LogError("SpawnPointsが少ないです！");
        }

        //現状プレイヤーは一人なので[0]
        _player = ServiceLoacator.ResolveAll<Player>()[0];
    }

    /// <summary>
    /// 各地点に敵を生成する
    /// </summary>
    void Spawn()
    {
        _spawnPoints.ForEach(spawnPoint => Instantiate(_spawnEnemy, spawnPoint, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log(dir);

        if (dir <= _activeDir)
        {
            Spawn();
            this.gameObject.SetActive(false);
        }
    }
}
