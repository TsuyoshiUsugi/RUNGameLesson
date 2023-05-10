using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ランゲームでプレイヤーが取得するオブジェクトを生成する
/// </summary>
public class ObjInstantiater : MonoBehaviour
{
    [Header("設定値")]
    [SerializeField] float _fieldLength = 100;
    [SerializeField] int _generateNum = 10;
    [SerializeField] List<GameObject> _Obstacles;
    //レーン数は３つ
    [SerializeField]　List<Vector3> _generateStartPoint = new List<Vector3> { 
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 0),
    };

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

        //横
        for (int i = 0; i < _generateStartPoint.Count; i++)
        {
            //縦
            for (int j = 0; j < _generateNum; j++)
            {
                Instantiate(_Obstacles[0], _generateStartPoint[i], Quaternion.identity);
                _generateStartPoint[i] = new Vector3(_generateStartPoint[i].x, _generateStartPoint[i].y, _generateStartPoint[i].z + dir);
            }
        }
    }
}
