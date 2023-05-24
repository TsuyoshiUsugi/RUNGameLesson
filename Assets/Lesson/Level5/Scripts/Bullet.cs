using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

/// <summary>
/// �e�ɕt����R���|�[�l���g
/// �������ɑ��N���X����ړ��������w�肵�A����
/// �w�肵�����Ԃŏ�����
/// </summary>
public class Bullet : MonoBehaviour, IHit
{
    int _damage = 1;
    public int Damage => _damage;
    float _speed = 0.1f;
    float _timdToDestroy = 3;

    Vector3 _dir = Vector3.right;
    float _width = 0;
    float _height = 0;
    List<GameObject> _targets;
    [SerializeField] BulletType _bulletType = BulletType.Circle;

    enum BulletType
    {
        Circle,
        Box,
    }

    void Awake()
    {
        ServiceLoacator.Register(this);
    }

    /// <summary>
    /// ���̃N���X�̃I�u�W�F�N�g�������ɑ��N���X����Ă΂��B
    /// �����ɁA�ڐG������s���ׂ��I�u�W�F�N�g�̃��X�g�ƒe�̐i�s���������
    /// </summary>
    /// <param name="targets"></param>
    /// <param name="dir"></param>
    public void InitializedBullet(List<GameObject> targets, Vector3 dir)
    {
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
        _targets = targets;
        _dir = dir;
    }

    void Update()
    {
        CountDeleteTime();
        Move();

        if (_bulletType == BulletType.Box)
        {
            var target = MyCollision.CollisionEnter(this.gameObject, _targets);
            foreach (var obj in _targets)
            {
                Debug.Log(obj.name);
                obj.GetComponent<IHit>().Hit(_damage);
            }
            Destroy(this.gameObject);
        }
        else
        {
            var target = MyCollision.CircleCollision(this.gameObject ,_targets);

            foreach (var obj in _targets)
            {
                Debug.Log(obj.name);
                obj.GetComponent<IHit>().Hit(_damage);
            }
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ���Ԃŏ��ł��鏈��
    /// </summary>
    void CountDeleteTime()
    {
        _timdToDestroy -= Time.deltaTime;
        if (_timdToDestroy < 0) Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.position += _dir * _speed;
    }

    public void Hit(int damage)
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        ServiceLoacator.Unregister(this);
    }
}
