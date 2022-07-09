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

    // T型でインスタンスを登録する
    public void Regist<T>(T instance)
    {
        throw new System.Exception("型とインスタンスを登録しよう！");
    }

    // T型で登録されたインスタンスを返す。
    public T GetInstance<T>()
    {
        throw new System.Exception("型とマッチしたインスタンスを返そう！　今回はマッチしないケースは考えなくてよいです。");
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        // T のインスタンスが先に Regist されているとは限らない
        // Regist されたら callbackを呼ぶメソッドを作ろう！
        throw new System.Exception("余裕があったら① T インスタンスが登録されたら、callbackを呼ぼう！\n [Hint]: ラムダ式もキャッシュする必要があるのでフィールドやRegist<T>の処理を増やそう！");
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        throw new System.Exception("余裕があったら②（授業でやることが無い人編）非同期系にしよう！\n [Hint]: 非同期は難しい・・・調べよう！意外と数行で実装できるよ！");
    }

    // シングルトンのやつ
    private static ServiceLocater _instance;
    public static ServiceLocater Instance => _instance ??= new ServiceLocater();
}
