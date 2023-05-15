using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// オブジェクトの生成を管理する
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [Header("設定値")]
    [SerializeField] float _fieldLength = 100;
    [SerializeField] int _generateNum = 10;
    [SerializeField] Vector3 _generateStartPoint = new Vector3();
    [SerializeField] List<GameObject> _Obstacles;
    int _leftLimit = -10;
    int _rightLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        InstatntitateObstacles();
    }

    /// <summary>
    /// 障害物を生成する
    /// </summary>
    void InstatntitateObstacles()
    {
        //生成間隔
        var dir = _fieldLength / _generateNum;

        //生成処理
        for (int i = 0; i < _generateNum; i++)
        {
            var obj = Instantiate(_Obstacles[0], _generateStartPoint, Quaternion.identity);

            _generateStartPoint = new Vector3(
                Random.Range(_leftLimit, _rightLimit)
                , _generateStartPoint.y
                , _generateStartPoint.z + dir);
        }

    }
}
