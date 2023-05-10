using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Q�[���Ńv���C���[���擾����I�u�W�F�N�g�𐶐�����
/// </summary>
public class ObjInstantiater : MonoBehaviour
{
    [Header("�ݒ�l")]
    [SerializeField] float _fieldLength = 100;
    [SerializeField] int _generateNum = 10;
    [SerializeField] List<GameObject> _Obstacles;
    //���[�����͂R��
    [SerializeField]�@List<Vector3> _generateStartPoint = new List<Vector3> { 
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
    /// ��Q���𐶐�����
    /// </summary>
    void InstatntitateObstacles()
    {
        //�����Ԋu
        var dir = _fieldLength / _generateNum;

        //��
        for (int i = 0; i < _generateStartPoint.Count; i++)
        {
            //�c
            for (int j = 0; j < _generateNum; j++)
            {
                Instantiate(_Obstacles[0], _generateStartPoint[i], Quaternion.identity);
                _generateStartPoint[i] = new Vector3(_generateStartPoint[i].x, _generateStartPoint[i].y, _generateStartPoint[i].z + dir);
            }
        }
    }
}
