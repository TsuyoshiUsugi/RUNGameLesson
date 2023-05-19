using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLoacator
{
    static readonly Dictionary<Type, List<object>> _container;

    static ServiceLoacator() => _container = new Dictionary<Type, List<object>>();

    /// <summary>
    /// �C���X�^���X�擾
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> ResolveAll<T>()
    {
        Type type = typeof(T);

        if (_container.ContainsKey(type))
        {
            List<object> instances = _container[type];
            List<T> typedInstances = new List<T>();

            foreach (var instance in instances)
            {
                typedInstances.Add((T)instance);
            }

            return typedInstances;
        }

        return new List<T>();
    }

    /// <summary>
    /// �C���X�^���X�o�^
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    public static void Register<T>(T service)
    {
        Type type = typeof(T);

        if (!_container.ContainsKey(type))
        {
            _container[type] = new List<object>();
             
        }

        _container[type].Add(service);
    }

    public static void Unregister<T>(T service)
    {
        Type type = typeof(T);

        if (_container.ContainsKey(type))
        {
            List<object> instances = _container[type];
            instances.Remove(service);

            // �Q�Ƃ���ɂȂ����ꍇ�̓G���g�����̂��폜
            if (instances.Count == 0)
            {
                _container.Remove(type);
            }   
        }
    }

    /// <summary>
    /// �o�^���Ă���C���X�^���X�S�폜
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void Clear<T>() => _container.Remove(typeof(T));
}
