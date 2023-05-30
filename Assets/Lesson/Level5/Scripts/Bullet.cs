using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

/// <summary>
/// 弾に付けるコンポーネント
/// 生成時に他クラスから移動方向を指定し、動く
/// 指定した時間で消える
/// </summary>
public class Bullet : MonoBehaviour, IHit, IMovable
{
    int _damage = 1;
    public int Damage => _damage;
    float _speed = 0.1f;
    float _timdToDestroy = 3;

    float _hitStopTime = 0.08f;
    Vector3 _dir = Vector3.right;
    float _width = 0;
    float _height = 0;
    List<GameObject> _targets = new List<GameObject>();
    [SerializeField] BulletType _bulletType = BulletType.Circle;

    bool _isHit = false;

    enum BulletType
    {
        Circle,
        Box,
    }

    void Awake()
    {
        ServiceLocator.Register(this);
    }

    /// <summary>
    /// このクラスのオブジェクト生成時に他クラスから呼ばれる。
    /// 引数に、接触判定を行うべきオブジェクトのリストと弾の進行方向を取る
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
        if (_isHit) return;

        CountDeleteTime();
        MovePos();

        if (_targets == null) return;

        CheckHit();
    }

    private void CheckHit()
    {
        if (_bulletType == BulletType.Box)
        {
            var target = MyCollision.CollisionEnter(this.gameObject, _targets);

            CallHit(target);
        }
        else if (_bulletType == BulletType.Circle)
        {
            var target = MyCollision.CircleCollision(this.gameObject, _targets);
            Debug.Log(_targets.Count);
            target.ForEach(a => Debug.Log(a));

            CallHit(target);
        }

        void CallHit(List<GameObject> target)
        {
            if (target.Count == 0) return;

            foreach (var obj in target)
            {
                Debug.Log(obj.name);
                obj.GetComponent<IHit>().Hit(_damage, transform.position);

                if (obj.GetComponent<Enemy>())
                {
                    var enemies = ServiceLocator.ResolveAll<Enemy>();
                    enemies.ForEach(enemy => enemy.gameObject.GetComponent<IMovable>().Stop(_hitStopTime));
                }
            }
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 時間で消滅する処理
    /// </summary>
    void CountDeleteTime()
    {
        _timdToDestroy -= Time.deltaTime;
        if (_timdToDestroy < 0) Destroy(this.gameObject);
    }

    private void MovePos()
    {
        transform.position += _dir * _speed;
    }

    public void Hit(int damage, Vector3 dir)
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister(this);
    }

    public void Stop(float time)
    {
        _isHit = true;
        StartCoroutine(CountHitStop(time));
    }

    IEnumerator CountHitStop(float time)
    {
        yield return new WaitForSeconds(time);
        _isHit = false;
    }
}
