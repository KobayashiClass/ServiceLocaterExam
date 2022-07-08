using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// このクラスを完成させよう！
public class ServiceLocater
{
    private ServiceLocater()
    {

    }

    public void Regist<T>(T instance)
    {
        throw new System.Exception("型とインスタンスを登録しよう！");
    }

    public T GetInstance<T>()
    {
        throw new System.Exception("型にあったインスタンスを返そう！無かったらnullを返そう！");
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        // T のインスタンスが先に Regist されているとは限らない
        // Regist されたら callbackを呼ぶメソッドを作ろう！
        throw new System.Exception("余裕があったら① T インスタンスが登録されたら、callbackを呼ぼう！");
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        throw new System.Exception("余裕があったら②（授業でやることが無い人編）非同期系にしよう");
    }

    private static ServiceLocater _instance;
    public static ServiceLocater Instance => _instance ??= new ServiceLocater();
}
