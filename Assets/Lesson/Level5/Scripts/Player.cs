using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    float _speed = 0.1f;
    Gravity _gravity;

    //ジャンプ関連
    [SerializeField] float _jumpHeight = 1.5f; //どれくらいの高さまで飛ぶか
    [SerializeField] float _jumpTime = 0.5f;　//どのくらいの時間飛ぶか
    float _jumpTimer = 0f;  //ジャンプしてからどれくらいの間飛んでいるか
    [SerializeField] bool _isJumping;
    int _maxJumpNum = 2;
    [SerializeField] int _currentJumpNum = 0;

    private void Start()
    {
        TryGetComponent(out Gravity gravity);
        _gravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        OnGround();
    }

    private void OnGround()
    {
        if (transform.position.y <= _gravity.GroundHeight)
        {
            _currentJumpNum = 0;
        }
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

        if (Input.GetKeyDown(KeyCode.Space) && _currentJumpNum < _maxJumpNum)
        {
            Jump();
            _currentJumpNum++;
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
            transform.position += new Vector3(0, verticalDistance, 0) * Time.deltaTime;
        }
        else
        {
            _isJumping = false;

        } 
    }
}
