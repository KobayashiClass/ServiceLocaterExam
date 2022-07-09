using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Start()
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

        if (!result) throw new Exception("[Step1] 結果が違います・・・");
        Debug.Log("[Step1] OKです！");

        Class2 ts = new Class2();
        IC2 ret = null;
        ServiceLocater.Instance.RequestInstance<IC2>((val) => ret = val);
        ServiceLocater.Instance.Regist<IC2>(ts);

        if (ret != ts) throw new Exception("[Step2] 結果が違います・・・");
        Debug.Log("[Step2] OKです！");

        async Task Ex3()
        {
            Class3 ts = new Class3();
            var task = ServiceLocater.Instance.GetInstanceAsync<IC3>();
            ServiceLocater.Instance.Regist<IC3>(ts);
            IC3 ret = await task;
            if (ret != ts) throw new Exception("[Step2] 結果が違います・・・");
            Debug.Log("[Step3] OKです！");
        }
        Ex3();
    }

    private interface IC1 { }
    private class Class1 : IC1 { }

    private interface IC2 { }
    private class Class2 : IC2 { }

    private interface IC3 { }
    private class Class3 : IC3 { }
}