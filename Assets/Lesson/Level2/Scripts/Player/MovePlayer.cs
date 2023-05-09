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
    [SerializeField] int _speed = 100;

    Vector3 _direction = new Vector3();

    void Awake()
    {
        if (!_rigidBody) _rigidBody = GetComponent<Rigidbody>();
        _direction = (_goal - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (transform.position.z > _goal.z)
        {
            StopObj();
        }
        else if (transform.position.z < _goal.z)
        {
            AutoMoveObj();
        }
    }

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g�������I�ɃS�[�������Ɉړ�������
    /// </summary>
    void AutoMoveObj()
    {
        _rigidBody.velocity = _direction * _speed * Time.deltaTime;
    }

    void StopObj()
    {
        _rigidBody.velocity = Vector3.zero;
        transform.position = _goal;
    }

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g�����E�Ɉړ�������
    /// </summary>
    public void HorizontalControl(int horizontal)
    {
        //�E
        if (horizontal == 1)
        {
            _rigidBody.AddForce(Vector3.right * _speed);
        }
        //��
        else if (horizontal == -1)
        {
            _rigidBody.AddForce(-Vector3.right * _speed);
        }
    }
}