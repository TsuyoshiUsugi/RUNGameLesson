using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] Rigidbody _rigidBody;

    [Header("設定値")]
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
    /// オブジェクトをゴールまで移動させる
    /// </summary>
    void MoveObj()
    {
        _rigidBody.velocity = (_goal.position - transform.position).normalized * _speed * Time.deltaTime;
    }
}