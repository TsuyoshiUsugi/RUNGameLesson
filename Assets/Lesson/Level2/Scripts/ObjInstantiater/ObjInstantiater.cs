using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �����Q�[���Ńv���C���[���擾����I�u�W�F�N�g�𐶐�����
/// </summary>
public class ObjInstantiater : MonoBehaviour
{
    [Header("�ݒ�l")]
    [SerializeField] float _fieldLength = 100;
    [SerializeField] int _generateNum = 10;
    [SerializeField] List<GameObject> _Obstacles;
    int _leftLimit = -10;
    int _rightLimit = 10;

    //���[�����͂R��
    [SerializeField] Vector3 _generateStartPoint = new Vector3();
    

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

        //��������
        for (int i = 0; i < _generateNum; i++)
        {
            var obj = Instantiate(_Obstacles[0], _generateStartPoint, Quaternion.identity);

            obj.GetComponent<FieldObject>().OnGetPoint.AsObservable();

            _generateStartPoint = new Vector3(
                Random.Range(_leftLimit, _rightLimit)
                , _generateStartPoint.y
                , _generateStartPoint.z + dir);
        }

    }
}
