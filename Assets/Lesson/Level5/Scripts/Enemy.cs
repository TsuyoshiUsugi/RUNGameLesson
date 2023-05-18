using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

/// <summary>
/// �G�I�u�W�F�N�g�ɕt����s���p�X�N���v�g
/// �v���C���[�Ƃ̋�����X�ȉ��ɂȂ�Ɖ�ʁ��e������
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] float _detectionDistance = 4;
    [SerializeField] GameObject _bullet;
    float _shootDur = 1;
    BoolReactiveProperty _isShoot = new BoolReactiveProperty();
 
    // Start is called before the first frame update
    void Start()
    {
        _isShoot.Where(x => x == true).Subscribe(_ => Shoot()).AddTo(this);
    }

    private void Update()
    {
        if (this.transform.position.x - _player.transform.position.x <= _detectionDistance)
        {
            _isShoot.Value = true;
        }
    }

    async void Shoot()
    {
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().Dir = Vector3.left;
        await UniTask.Delay(TimeSpan.FromSeconds(_shootDur));
        Shoot();
    }
}
