using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 弾に付けるコンポーネント
/// 生成時に他クラスから移動方向を指定し、動く
/// 指定した時間で消える
/// </summary>
public class Bullet : MonoBehaviour
{
    int _damage = 1;
    public int Damage => _damage;
    float _speed = 0.1f;
    float _timdToDestroy = 3;

    Vector3 _dir = Vector3.right;
    float _width = 0;
    float _height = 0;
    List<GameObject> _targets;

    private void Start()
    {
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void InitializedBullet(List<GameObject> targets, Vector3 dir)
    {
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
        _targets = targets;
        _dir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        CountDeleteTime();
        Move();
        CollisionEnter(_targets);
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

    private void CollisionEnter(List<GameObject> otherObjects)
    {
        if (otherObjects == null) return;

        foreach (var obj in otherObjects)
        {
            if (obj == null) continue;

            var otherObjPos = obj.transform.position;
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
}
