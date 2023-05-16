using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] Vector3 _groundPos = new Vector3();
    Vector3 _gravity = new Vector3(0, 9.8f, 0);

    private void FixedUpdate()
    {
        this.transform.position -= _gravity;

        if (transform.position.y <= _groundPos.y)
        {
            this.transform.position = _groundPos;
        }
        
    }
}
