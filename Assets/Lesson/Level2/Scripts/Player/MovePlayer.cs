using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを動かすスクリプト
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] Rigidbody _rigidBody;

    [Header("設定値")]
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
    /// プレイヤーオブジェクトを自動的にゴール方向に移動させる
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
    /// プレイヤーオブジェクトを左右に移動させる
    /// </summary>
    public void HorizontalControl(int horizontal)
    {
        //右
        if (horizontal == 1)
        {
            _rigidBody.AddForce(Vector3.right * _speed);
        }
        //左
        else if (horizontal == -1)
        {
            _rigidBody.AddForce(-Vector3.right * _speed);
        }
    }
}