using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t�B�[���h��ɐ��������l���p�̃I�u�W�F�N�g
/// </summary>
public class FieldObject : MonoBehaviour
{
    public event Action OnGetPoint;

    private void Start()
    {
        if (!GetComponent<BoxCollider>())
        {
            var col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnGetPoint != null) OnGetPoint();
        ScoreManager.Instance.AddScore();
        gameObject.SetActive(false);
    }
}
