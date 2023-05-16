using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    float _groundHeight = 0;
    public float GroundHeight => _groundHeight;

    Vector3 _gravity = new Vector3(0, 9.8f, 0);

    private void FixedUpdate()
    {
        this.transform.position -= _gravity * Time.deltaTime;

        if (transform.position.y <= _groundHeight)
        {
            this.transform.position = new Vector3(transform.position.x, _groundHeight, transform.position.z);
        }
        
    }
}
