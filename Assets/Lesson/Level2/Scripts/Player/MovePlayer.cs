using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�𓮂����X�N���v�g
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] Rigidbody _rigidBody;

    [Header("�ݒ�l")]
    [SerializeField] Vector3 _goal = new Vector3(0, 1, 52.5f);
    [SerializeField] int _verticalSpeed = 100;
    [SerializeField] int _HorizontalSpeed = 100;
    [SerializeField] float _leftLimit = 0;
    [SerializeField] float _rightLimit = 0;

    Vector3 _direction = new Vector3();

    void Awake()
    {
        if (!_rigidBody) _rigidBody = GetComponent<Rigidbody>();
        _direction = (_goal - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        AutoMoveObj();
    }

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g�������I�ɃS�[�������Ɉړ�������
    /// </summary>
    void AutoMoveObj()
    {
        //�S�[�����C�����z������~�܂�
        if (transform.position.z > _goal.z)
        {
            _rigidBody.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, _goal.y, _goal.z);
        }
        else if (transform.position.z < _goal.z)
        {
            _rigidBody.velocity = _direction * _verticalSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g�����E�Ɉړ�������
    /// </summary>
    public void HorizontalControl(int horizontal)
    {
        //�E
        if (horizontal == 1)
        {
            _rigidBody.AddForce(Vector3.right * _verticalSpeed);
        }
        //��
        else if (horizontal == -1)
        {
            _rigidBody.AddForce(-Vector3.right * _verticalSpeed);
        }
    }
}