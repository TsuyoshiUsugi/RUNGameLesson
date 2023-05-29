using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Xml.Linq;
=======
>>>>>>> origin/master
using UniRx;
using UnityEngine;

/// <summary>
/// レーザー銃のスクリプト
/// </summary>
public class LaserGun : MonoBehaviour
{
    [SerializeField] GameObject _laser;
    [SerializeField] int _damage = 3;

    [SerializeField] float _onKeyDownTimer = 0;
    float _showLaserTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _laser.SetActive(false);
<<<<<<< HEAD

        var myObjWidth = _laser.GetComponent<Transform>().localScale.x;
        var myObjHeight = _laser.GetComponent<Transform>().localScale.y;
        Debug.Log($"H{myObjHeight} W{myObjWidth}");
=======
>>>>>>> origin/master
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    /// <summary>
    /// レーザーを発射する処理
    /// 12フレーム後にoff
    /// </summary>
    IEnumerator ShootLaser()
    {
        _laser.SetActive(true);
        var target = new List<GameObject>();
        ServiceLoacator.ResolveAll<Enemy>().ForEach(enemy => target.Add(enemy.gameObject));
        ServiceLoacator.ResolveAll<Bullet>().ForEach(bullet => target.Add(bullet.gameObject));

        var hit = MyCollision.CollisionEnter(_laser.gameObject, target);
        hit.ForEach(hit => hit.GetComponent<IHit>().Hit(_damage));
        yield return new WaitForSeconds(_showLaserTime);
        _laser.SetActive(false);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _onKeyDownTimer += Time.deltaTime;
        }

        if (_onKeyDownTimer >= 1)
        {
            StartCoroutine(ShootLaser());
            _onKeyDownTimer = 0;
        }

    }
}
