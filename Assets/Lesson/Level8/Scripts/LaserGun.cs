using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// レーザー銃のスクリプト
/// </summary>
public class LaserGun : MonoBehaviour
{
    [SerializeField] GameObject _laser;
    [SerializeField] float _damage = 3;

    [SerializeField] float _onKeyDownTimer = 0;
    float _showLaserTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _laser.SetActive(false);
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
