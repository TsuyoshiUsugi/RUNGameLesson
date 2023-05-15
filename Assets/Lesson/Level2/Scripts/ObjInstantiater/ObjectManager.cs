using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// オブジェクトの生成を管理する
/// オブジェクトはランダムな位置に連続して決められた数生成される
/// 
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [Header("設定値")]
    [SerializeField] float _fieldLength = 100;
    [SerializeField] int _generateObjNum = 10;
    [SerializeField] Vector3 _generateStartPoint = new Vector3();
    [SerializeField] List<GameObject> _Obstacles;
    [SerializeField] int _consecutiveGenerationNum = 3;

    List<int> _lanes = new List<int> { -6, 0, 6};

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
        var dir = _fieldLength / _generateObjNum;
        var groupNum = _generateObjNum / _consecutiveGenerationNum;

        var lane = 0;

        for (int i = 0; i < _generateObjNum; i++)
        {
            if (i == 0 || i % groupNum == 0)
            {
                lane = Random.Range(0, _lanes.Count);
                Debug.Log($"{i}:{lane}");
            }

            if (i == 0)
            {
                _generateStartPoint = new Vector3(
                    _lanes[lane]
                    , _generateStartPoint.y
                    , _generateStartPoint.z);
            }
            else
            {
                _generateStartPoint = new Vector3(
                    _lanes[lane]
                    , _generateStartPoint.y
                    , _generateStartPoint.z + dir);
            }

            var obj = Instantiate(_Obstacles[0], _generateStartPoint, Quaternion.identity);
        }

    }
}
