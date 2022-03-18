using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MainScript : MonoBehaviour
{
    //public InputField X1; ��� ����� � ������  
    //MinX = double.Parse(X1.text);

    public ToggleScript Body;
    public ToggleScript BagSize;
    public ToggleScript AmountSeats;
    public ToggleScript AmountDoors;
    public ToggleScript Country;
    public ToggleScript Mark;
    public ToggleScript TypeOfDrive;
    public ToggleScript FuelType;
    public ToggleScript Transmission;
    public ToggleScript ElectricWindows;
    public ToggleScript Climate;
    public ToggleScript Roof;
    public ToggleScript Security;
    public ToggleScript Cabin;
    public ToggleScript Multimedia;
    public ToggleScript Assist;
    public ToggleScript Airbags;

    public MinMaxScript Power;
    public MinMaxScript Size;
    public MinMaxScript Acceleration;
    public MinMaxScript Speed;
    public MinMaxScript FuelConsumption;
    public MinMaxScript PowerReserve;
    public MinMaxScript Cost;
    public MinMaxScript RoadP;
    public MinMaxScript Year;


    string body;

    [Serializable]
    public class Auto
    {
        public string name;
        public string link;
        public string image;
    }

    public string server;

    public OutputScript output;
    public async UniTask GetData()
    {
        var data0 = Get();
        output.Destroy();
        var data = FixString(await data0);
        Auto[] autos = JsonHelper.FromJson<Auto>(data);
        var CreateCards = autos.Select(async card =>
        {
            await output.Create(card.name, card.link, card.image);
        });
        await UniTask.WhenAll(CreateCards);
    }
    public void Search()
    {
        output.Destroy();
        Debug.Log(BagSize.Get_Names());
        Debug.Log(Power.getValues());
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
        form.AddField("Body", Body.Get_Names());
        form.AddField("BagSize", BagSize.Get_Names());
        form.AddField("AmountSeats", AmountSeats.Get_Names());
        form.AddField("AmountDoors", AmountDoors.Get_Names());
        form.AddField("Country", Country.Get_Names());
        form.AddField("Mark", Mark.Get_Names());
        form.AddField("TypeOfDrive", TypeOfDrive.Get_Names());
        form.AddField("FuelType", FuelType.Get_Names());
        form.AddField("ElectricWindows", ElectricWindows.Get_Names());
        form.AddField("Climate", Climate.Get_Names());
        form.AddField("Roof", Roof.Get_Names());
        form.AddField("Security", Security.Get_Names());
        form.AddField("Cabin", Cabin.Get_Names());
        form.AddField("Multimedia", Multimedia.Get_Names());
        form.AddField("Assist", Assist.Get_Names());
        form.AddField("Airbags", Airbags.Get_Names());
        form.AddField("Power", Power.getValues());
        form.AddField("Size", Size.getValues());
        form.AddField("Acceleration", Acceleration.getValues());
        form.AddField("Speed", Speed.getValues());
        form.AddField("FuelConsumption", FuelConsumption.getValues());
        form.AddField("PowerReserve", PowerReserve.getValues());
        form.AddField("Cost", Cost.getValues());
        form.AddField("RoadP", RoadP.getValues());
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