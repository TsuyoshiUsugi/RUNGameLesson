using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �e�ɕt����R���|�[�l���g
/// �������ɑ��N���X����ړ��������w�肵�A����
/// �w�肵�����Ԃŏ�����
/// </summary>
public class Bullet : MonoBehaviour, ICollision
{
    int _damage = 1;
    public int Damage => _damage;
    float _speed = 0.1f;
    float _timdToDestroy = 3;

    Vector3 _dir = Vector3.right;
    float _width = 0;
    float _height = 0;
    List<GameObject> _targets;

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
        //CollisionEnter(_targets);
        CircleCollision(_targets);
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

    /// <summary>
    /// ���݂�����`�̎��̐ڐG���m�֐�
    /// </summary>
    /// <param name="otherObjects"></param>
    public void CollisionEnter(List<GameObject> otherObjects)
    {
        if (otherObjects == null) return;

        foreach (var obj in otherObjects)
        {
            if (obj == null) continue;

            var otherObjPos = obj.transform.position;

            //�����̓L���b�V�����ׂ�
            var width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;

            var xDir = Mathf.Abs(this.transform.position.x - otherObjPos.x);
            var yDir = Mathf.Abs(this.transform.position.y - otherObjPos.y);

            var widthSum = Mathf.Abs(_width - width);
            var heightSum = Mathf.Abs(_height - height);

            if (xDir <= widthSum && yDir <= heightSum)
            {
                obj.gameObject.GetComponent<IHit>().Hit(_damage);
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// ���I�W�F�N�g���ۂœ�����I�u�W�F�N�g����`�̎��̐ڐG����
    /// </summary>
    /// <param name="otherObjects"></param>
    void CircleCollision(List<GameObject> otherObjects) 
    {
        if (otherObjects == null) return;


        foreach (var obj in otherObjects)
        {
            if (obj == null) continue;

            var otherObjPos = obj.transform.position;
            var circleLocalPos = transform.position - otherObjPos;

            //�����̓L���b�V�����ׂ�
            var width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;

            var halfWidth = width / 2;
            var halfheight = height / 2;

            var nearestX = Mathf.Clamp(circleLocalPos.x, -halfWidth, halfWidth);
            var nearestY = Mathf.Clamp(circleLocalPos.y, -halfheight, halfheight);

            float distanceX = circleLocalPos.x - nearestX;
            float distanceY = circleLocalPos.y - nearestY;
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

            if (distance < _width / 2)
            {
                obj.gameObject.GetComponent<IHit>().Hit(_damage);
                Destroy(this.gameObject);
            }
        }
    }
}
