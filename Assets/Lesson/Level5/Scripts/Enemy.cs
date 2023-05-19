using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using TMPro;

/// <summary>
/// �G�I�u�W�F�N�g�ɕt����s���p�X�N���v�g
/// �v���C���[�Ƃ̋�����X�ȉ��ɂȂ�Ɖ�ʁ��e����[��
/// </summary>
public class Enemy : MonoBehaviour, IHit
{
    [SerializeField] Player _player;
    [SerializeField] float _detectionDistance = 4;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _hp = 5;
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
        var targetList = new List<GameObject>() { _player.gameObject};
        bullet.GetComponent<Bullet>().InitializedBullet(targetList, Vector3.left);
        await UniTask.Delay(TimeSpan.FromSeconds(_shootDur));
        Shoot();
    }

    public void Hit(int damage)
    {
        _hp -= damage;
        if (_hp <= 0) Destroy(this.gameObject);
    }
}
