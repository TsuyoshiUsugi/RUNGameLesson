using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHit
{
    float _hp = 5;
    float _speed = 0.1f;
    [SerializeField] Vector3 _mousePos = new Vector3();

    [SerializeField] GameObject _mouseCursor;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _laserGun;

    [Header("ノックバック")]
    [SerializeField] float _knockBackPow = 1;
    [SerializeField] int _knockBackFrame = 8;
    Gravity _gravity;
    List<Enemy> _enemy;
    bool _isKnockBack = false;

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
        float angle = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x);
        Vector3 eulerAngle = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
        transform.eulerAngles = eulerAngle ;
        transform.rotation = Quaternion.Euler(eulerAngle);

        _laserGun.transform.rotation = this.transform.rotation;
    }

    void ShowCursor()
    {
        Vector3 mouseScreenPos = _mousePos;
        mouseScreenPos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;
        _mouseCursor.transform.position = mouseWorldPos;
    }

    public void Hit(int damage, Vector3 dir)
    {
        if (_isKnockBack) return;
        _hp -= damage;

        _isKnockBack = true;
        StartCoroutine(nameof(KnockBack), dir);
    }

    IEnumerator KnockBack(Vector3 dir)
    {

        for (int i = 0; i < _knockBackFrame; i++)
        {
            this.transform.position += (transform.position - dir).normalized * _knockBackPow * Time.deltaTime;
            yield return null;
        }
        _isKnockBack = false;
    }

    private void OnDisable()
    {
        ServiceLoacator.Register(this);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
