using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�����܂ŋ߂Â��Ǝw�肵���n�_�ɓG����Ăɐ�������
/// ��x�����N��
/// </summary>
public class SpwanPoints2 : MonoBehaviour
{
    [SerializeField] GameObject _spawnEnemy;
    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] float _activeDir = 2;
    private readonly int _leastSpawnPointNum = 2;
    Player _player;

    private void Start()
    {
        if( _spawnPoints.Count < _leastSpawnPointNum)
        {
            Debug.LogError("SpawnPoints�����Ȃ��ł��I");
        }

        //����v���C���[�͈�l�Ȃ̂�[0]
        _player = ServiceLoacator.ResolveAll<Player>()[0];
    }

    /// <summary>
    /// �e�n�_�ɓG�𐶐�����
    /// </summary>
    void Spawn()
    {
        _spawnPoints.ForEach(spawnPoint => Instantiate(_spawnEnemy, spawnPoint, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log(dir);

        if (dir <= _activeDir)
        {
            Spawn();
            this.gameObject.SetActive(false);
        }
    }
}
