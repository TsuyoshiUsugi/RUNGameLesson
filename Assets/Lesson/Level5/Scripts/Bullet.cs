using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _speed = 0.1f;
    Vector3 _dir = Vector3.right;
    public Vector3 Dir { get => _dir; set => _dir = value; }
    float _timdToDestroy = 3;

    // Update is called once per frame
    void Update()
    {
        Move();
        CountDeleteTime();
    }

    void CountDeleteTime()
    {
        _timdToDestroy -= Time.deltaTime;
        if (_timdToDestroy < 0) Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.position += _dir * _speed;
    }
}
