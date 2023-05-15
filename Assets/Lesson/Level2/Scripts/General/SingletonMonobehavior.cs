using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
                if (!_instance) Debug.LogError($"{t} Ç™ÉVÅ[Éìè„Ç…Ç†ÇËÇ‹ÇπÇÒ");
            }

            return _instance;
        }
        
    }

    protected virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
