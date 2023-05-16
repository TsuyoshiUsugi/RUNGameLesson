using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    float _speed = 0.1f;

    //�W�����v�֘A
    float _jumpHeight = 0.1f; //�ǂꂭ�炢�̍����܂Ŕ�Ԃ�
    float _jumpTime = 0.5f;�@//�ǂ̂��炢�̎��Ԕ�Ԃ�
    float _jumpTimer = 0f;  //�W�����v���Ă���ǂꂭ�炢�̊Ԕ��ł��邩
    [SerializeField] bool _isJumping;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shot();
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            Jump();
        }

        if (_isJumping)
        {
            PerformJump();
        }


    }

    void Shot()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
    }

    void Move(bool right)
    {
        if (right)
        {
            transform.position += Vector3.right * _speed;
        }
        else
        {
            transform.position -= Vector3.right * _speed;
        }
    }

    void Jump()
    {
        _isJumping = true;
        _jumpTimer = 0f;
    }

    void PerformJump()
    {
        _jumpTimer += Time.deltaTime;

        if (_jumpTimer <= _jumpTime)
        {
            float jumpProgress = _jumpTimer / _jumpTime;
            var verticalDistance = Mathf.Sin(jumpProgress * Mathf.PI) * _jumpHeight;
            transform.position += new Vector3(0, verticalDistance, 0);
        }
        else
        {
            _isJumping = false;
        }
    }
}
