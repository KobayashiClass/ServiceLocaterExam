using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Start()
    {
        try
        {
            int ts1 = 10;
            string ts2 = "hello service location";
            Class1 ts3 = new Class1();

            ServiceLocater.Instance.Regist<int>(ts1);
            ServiceLocater.Instance.Regist<string>(ts2);
            ServiceLocater.Instance.Regist<IC1>(ts3);
            
            bool result = true;
            result = result && ServiceLocater.Instance.GetInstance<string>() == ts2;
            result = result && ServiceLocater.Instance.GetInstance<IC1>() == ts3;
            result = result && ServiceLocater.Instance.GetInstance<int>() == ts1;

            if (!result) throw new Exception();
            Debug.Log("[Step1] OK�ł��I");
        }
        catch
        {
            Debug.LogWarning("[Step1] Regist<T>��GetInstance<T>�����������I");
            return;
        }

        try
        {
            Class2 ts = new Class2();
            IC2 ret = null;
            ServiceLocater.Instance.RequestInstance<IC2>((val) => ret = val);
            ServiceLocater.Instance.Regist<IC2>(ts);

            if (ret != ts) throw new Exception();
            Debug.Log("[Step2] OK�ł��I");
        }
        catch
        {
            Debug.LogWarning("[Step2] RequestInstance<T>�����������I [Hint]: �����_�����L���b�V������K�v������̂Ńt�B�[���h�𑝂₻���I");
            return;
        }

        try
        {
            Class3 ts = new Class3();
            async Task Get()
            {
                IC3 ret = await ServiceLocater.Instance.GetInstanceAsync<IC3>();
                if (ret != ts) throw new Exception();
                Debug.Log("[Step3] OK�ł��I");
            }
            Get();
            ServiceLocater.Instance.Regist<IC3>(ts);
        }
        catch
        {
            Debug.LogWarning("[Step3] GetInstanceAsync<T>�����������I [Hint]: �񓯊��͓���A���ׂĂ݂悤�I");
            return;
        }
    }

    private interface IC1 { }
    private class Class1 : IC1 { }

    private interface IC2 { }
    private class Class2 : IC2 { }

    private interface IC3 { }
    private class Class3 : IC3 { }
}