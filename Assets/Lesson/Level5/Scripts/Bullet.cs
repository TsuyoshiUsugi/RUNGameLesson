using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _speed = 0.1f;
    Vector3 _dir = Vector3.right;

    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.right * _speed;
    }
}
