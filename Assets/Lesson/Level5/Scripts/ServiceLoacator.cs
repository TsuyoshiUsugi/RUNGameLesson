using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLoacator
{
    static readonly Dictionary<Type, object> _container;

    static ServiceLoacator() => _container = new Dictionary<Type, object>();

    /// <summary>
    /// �C���X�^���X�擾
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Resolve<T>() => (T)_container[typeof(T)];

    /// <summary>
    /// �C���X�^���X�o�^
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public static void Register<T>(T instance) => _container[typeof(T)] = instance;

    /// <summary>
    /// �C���X�^���X�̓o�^����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public static void UnRegister<T>(T instance)
    {
        if (Equals(_container[typeof(T)], instance)) _container.Remove(typeof(T));
    }

    /// <summary>
    /// �o�^���Ă���C���X�^���X�S�폜
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void Clear<T>() => _container.Remove(typeof(T));
}
