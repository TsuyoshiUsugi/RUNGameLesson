using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �I�u�W�F�N�g�̐������Ǘ�����
/// �I�u�W�F�N�g�̓����_���Ȉʒu�ɘA�����Č��߂�ꂽ�����������
/// 
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [Header("�ݒ�l")]
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
    /// ��Q���𐶐�����
    /// </summary>
    void InstatntitateObstacles()
    {
        //�����Ԋu
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
