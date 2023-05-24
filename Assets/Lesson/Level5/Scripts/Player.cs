using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour, IHit
{
    float _hp = 5;
    float _speed = 0.1f;
    Vector3 _mousePos = new Vector3();

    [SerializeField] GameObject _mouseCursor;
    [SerializeField] GameObject _bullet;
    Gravity _gravity;
    List<Enemy> _enemy;

    //ジャンプ関連
    [SerializeField] float _jumpHeight = 1.5f; //どれくらいの高さまで飛ぶか
    [SerializeField] float _jumpTime = 0.5f;　//どのくらいの時間飛ぶか
    float _jumpTimer = 0f;  //ジャンプしてからどれくらいの間飛んでいるか
    [SerializeField] bool _isJumping;
    int _maxJumpNum = 2;
    [SerializeField] int _currentJumpNum = 0;

    void Awake()
    {
        ServiceLoacator.Register(this);
    }

    private void Start()
    {
        TryGetComponent(out Gravity gravity);
        _gravity = gravity;

        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ShowCursor();
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

        _mousePos = Input.mousePosition;

        Rotate();
    }

    void Shot()
    {
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
        var enemies = new List<GameObject>();
        _enemy = ServiceLoacator.ResolveAll<Enemy>();
        foreach (var enemy in _enemy)
        {
            enemies.Add(enemy.gameObject);
        }

        var dir = (_mouseCursor.transform.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().InitializedBullet(enemies, dir);
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

    void Rotate()
    {
        float angle = Mathf.Atan2(_mousePos.x, _mousePos.y);
        Vector3 eulerAngle = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(-eulerAngle);
    }

    void ShowCursor()
    {
        var _mouseCursorPos = Camera.main.ScreenToWorldPoint(_mousePos);
        _mouseCursorPos.z = 0;
        _mouseCursor.transform.position = _mouseCursorPos;
    }

    public void Hit(int damage)
    {
        _hp -= damage;
    }

    private void OnDisable()
    {
        ServiceLoacator.Register(this);
    }
}
