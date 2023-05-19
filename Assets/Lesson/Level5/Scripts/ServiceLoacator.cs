using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLoacator
{
    static readonly Dictionary<Type, object> _container;

    static ServiceLoacator() => _container = new Dictionary<Type, object>();

    /// <summary>
    /// インスタンス取得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Resolve<T>() => (T)_container[typeof(T)];

    /// <summary>
    /// インスタンス登録
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public static void Register<T>(T instance) => _container[typeof(T)] = instance;

    /// <summary>
    /// インスタンスの登録解除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public static void UnRegister<T>(T instance)
    {
        if (Equals(_container[typeof(T)], instance)) _container.Remove(typeof(T));
    }

    /// <summary>
    /// 登録しているインスタンス全削除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void Clear<T>() => _container.Remove(typeof(T));
}
