using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MainScript : MonoBehaviour
{
    [Serializable]
    public class Auto
    {
        public string name;
        public string link;
        public string image;
    }

    public string server;
    public string transmisson;

    public OutputScript output;
    public async UniTask GetData()
    {
        var data0 = Get();
        output.Destroy();
        var data = FixString(await data0);
        Debug.Log(data);
        Auto[] autos = JsonHelper.FromJson<Auto>(data);
        var CreateCards = autos.Select(async card =>
        {
            Debug.Log(card.image);
            await output.Create(card.name, card.link, card.image);
        });
        await UniTask.WhenAll(CreateCards);
    }
    public void Search()
    {
        GetData();
    }

    public string FixString(string data)
    {
        var str = "{\"Items\":" + data + "}";
        return str;
    }

    public void Exit()
    {
        output.Destroy();
        Application.Quit();
    }

    private async UniTask<string> Get(CancellationToken cancellationtoken = default)
    {
        WWWForm form = new WWWForm();
        transmisson = "[\"Автоматическая\", \"Механика\"]";
        form.AddField("Transmisson", transmisson);
        var www = UnityWebRequest.Post(server, form);
        await www.SendWebRequest().WithCancellation(cancellationtoken);
        return www.result == UnityWebRequest.Result.Success ? www.downloadHandler.text : null;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}