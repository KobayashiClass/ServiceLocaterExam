using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// ���̃N���X�����������悤�I
public class ServiceLocater
{
    private ServiceLocater()
    {

    }

    public void Regist<T>(T instance)
    {
        throw new System.Exception("�^�ƃC���X�^���X��o�^���悤�I");
    }

    public T GetInstance<T>()
    {
        throw new System.Exception("�^�ɂ������C���X�^���X��Ԃ����I����������null��Ԃ����I");
    }

    public void RequestInstance<T>(System.Action<T> callback)
    {
        // T �̃C���X�^���X����� Regist ����Ă���Ƃ͌���Ȃ�
        // Regist ���ꂽ�� callback���Ăԃ��\�b�h����낤�I
        throw new System.Exception("�]�T����������@ T �C���X�^���X���o�^���ꂽ��Acallback���Ăڂ��I");
    }

    public async Task<T> GetInstanceAsync<T>()
    {
        throw new System.Exception("�]�T����������A�i���Ƃł�邱�Ƃ������l�ҁj�񓯊��n�ɂ��悤");
    }

    private static ServiceLocater _instance;
    public static ServiceLocater Instance => _instance ??= new ServiceLocater();
}
