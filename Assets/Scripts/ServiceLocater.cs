using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// このクラスを完成させよう！
public class ServiceLocater
{
#if !STEP1_DICTIONARY // Dictionaryで解決するパターン

    // System.Typeをキーにとる辞書型　しぶしぶobjectでキャッシュ
    Dictionary<Type, object> instanceByType;

    private ServiceLocater()
    {
        instanceByType = new Dictionary<Type, object>();
    }

    public void Regist<T>(T instance)
    {
        var type = typeof(T);
        instanceByType.Add(type, instance);
    }

    public T GetInstance<T>()
    {
        return (T)instanceByType[typeof(T)];
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        throw new Exception();
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        throw new Exception();
    }

#elif !STEP1_STATICCACHE // 天才的なやり方　非常に高速

    private static class Cacher<T>
    {
        public static T instance;
    }

    private ServiceLocater()
    {
    
    }

    public void Regist<T>(T instance)
    {
        Cacher<T>.instance = instance;
    }

    public T GetInstance<T>()
    {
        return Cacher<T>.instance;
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        throw new Exception();
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        throw new Exception();
    }

#elif !ALLSTEP_DICTIONARY // 追加問題 Dictionary　実装例
    Dictionary<Type, object> instanceByType;
    Dictionary<Type, System.Action<object>> onRegistByType;

    private ServiceLocater()
    {
        instanceByType = new Dictionary<Type, object>();
        onRegistByType = new Dictionary<Type, Action<object>>();
    }

    public void Regist<T>(T instance)
    {
        var type = typeof(T);
        instanceByType.Add(type, instance);

        if (onRegistByType.ContainsKey(type))
        {
            onRegistByType[type](instance);
            onRegistByType.Remove(type);
        }
    }

    public T GetInstance<T>()
    {
        return (T)instanceByType[typeof(T)];
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        var type = typeof(T);
        if (instanceByType.TryGetValue(type, out var val))
        {
            callback((T)val);
            return;
        }

        if (onRegistByType.ContainsKey(type))
        {
            onRegistByType[type] += (obj) => callback((T)obj);
        }
        else
        {
            onRegistByType.Add(type, (obj) => callback((T)obj));
        }
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        TaskCompletionSource<T> tsc = new TaskCompletionSource<T>();
        RequestInstance<T>(tsc.SetResult);
        return await tsc.Task;
    }
#else // 追加問題 Staticジェネリックのキャッシュ　実装例
    
    private static class Cacher<T>
    {
        public static T instance;
        public static Action<T> onRegist;
    }

    private ServiceLocater()
    {
    
    }

    // T型でインスタンスを登録する
    public void Regist<T>(T instance)
    {
        Cacher<T>.instance = instance;
        if (Cacher<T>.onRegist != null)
        {
            Cacher<T>.onRegist(instance);
            Cacher<T>.onRegist = null;
        }
    }

    // T型で登録されたインスタンスを返す。
    public T GetInstance<T>()
    {
        return Cacher<T>.instance;
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        // この時 Tがstructで破綻、実戦では必ず型制約を設けるべし

        if (Cacher<T>.instance != null) callback(Cacher<T>.instance);
        else Cacher<T>.onRegist += callback;
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        TaskCompletionSource<T> tsc = new TaskCompletionSource<T>();
        RequestInstance<T>(tsc.SetResult);
        return await tsc.Task;
    }

#endif

    private static ServiceLocater _instance;
    public static ServiceLocater Instance => _instance ??= new ServiceLocater();
}
