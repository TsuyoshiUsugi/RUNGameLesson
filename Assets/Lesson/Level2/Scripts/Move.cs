using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] Rigidbody _rigidBody;

    [Header("�ݒ�l")]
    [SerializeField] Transform _goal;
    [SerializeField] int _speed = 5;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObj();

    }

    /// <summary>
    /// �I�u�W�F�N�g���S�[���܂ňړ�������
    /// </summary>
    void MoveObj()
    {
        _rigidBody.velocity = (_goal.position - transform.position).normalized * _speed * Time.deltaTime;
    }
}