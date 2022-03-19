using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{ 
    public GameObject Technical;
    public GameObject Ext;
    public GameObject Equip;

    public SortScript Sort;
    public Dropdown Preset;

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


    [Serializable]
    public class Auto
    {
        public string name;
        public string link;
        public string image;
        public string cost;
        public double power;
    }

    public string server;

    public OutputScript output;
    public async UniTask GetData()
    {
        var data0 = Get();
        output.Destroy();
        var data = FixString(await data0);
        if (data == "{\"Items\":Машины не найдены}" || data == "{\"Items\":[]}")
        {
            output.Create("Машины не найдены", "https://ukr.host/kb/wp-content/uploads/2018/05/404.jpg", "https://ukr.host/kb/wp-content/uploads/2018/05/404.jpg", "");
        }
        else if (data == "{\"Items\":Неправильно переданы данные}")
        {
            output.Create("Ошибка в передаче данных", "https://cleverics.ru/digital/wp-content/uploads/2014/03/error.png", "https://cleverics.ru/digital/wp-content/uploads/2014/03/error.png", "");
        }
        else
        {
            Auto[] autos = JsonHelper.FromJson<Auto>(data);
            var CreateCards = autos.Select(async card =>
            {
                await output.Create(card.name, card.link, card.image, "от " + card.cost + " руб.");
            });
            await UniTask.WhenAll(CreateCards);
        }
    }

    private void Start()
    {

    }

    public void Search()
    {
        output.Destroy();
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
        form.AddField("SQL", CreateQuery());
        var www = UnityWebRequest.Post(server, form);
        await www.SendWebRequest().WithCancellation(cancellationtoken);
        return www.result == UnityWebRequest.Result.Success ? www.downloadHandler.text : null;
    }

    private string CreateQuery()
    {
        string query = "SELECT name, link, image, cost, power FROM auto WHERE " + Body.Get_Names() + " AND " +
                                                                     BagSize.Get_Names() + " AND " +
                                                                     AmountSeats.Get_Names() + " AND " +
                                                                     AmountDoors.Get_Names() + " AND " +
                                                                     Country.Get_Names() + " AND " +
                                                                     Mark.Get_Names() + " AND " +
                                                                     TypeOfDrive.Get_Names() + " AND " +
                                                                     FuelType.Get_Names() + " AND " +
                                                                     ElectricWindows.Get_Names() + " AND " +
                                                                     Climate.Get_Names() + " AND " +
                                                                     Roof.Get_Names() + " AND " +
                                                                     Security.Get_Names() + " AND " +
                                                                     Cabin.Get_Names() + " AND " +
                                                                     Multimedia.Get_Names() + " AND " +
                                                                     Assist.Get_Names() + " AND " +
                                                                     Airbags.Get_Names() + " AND " +
                                                                     Transmission.Get_Names() + " AND " +
                                                                     Power.getValues() + " AND " +
                                                                     Size.getValues() + " AND " +
                                                                     Acceleration.getValues() + " AND " +
                                                                     Speed.getValues() + " AND " +
                                                                     FuelConsumption.getValues() + " AND " +
                                                                     PowerReserve.getValues() + " AND " +
                                                                     Cost.getValues() + " AND " +
                                                                     RoadP.getValues() + " AND " +
                                                                     Year.getValues() + " " +
                                                                     Sort.Get_Sort();
        return query;
    }

    public void Presets()
    {
        int value = Preset.value;

        switch (value)
        {
            case 1:
                Default();
                Family();
                break;
            case 2:
                Default();
                Style();
                break;
            default:
                Default();
                break;
        }
    }

    private void Family()
    {
        string[] body ={ "Седан", "Хетчбэк", "Универсал", "Внедорожник", "Кроссовер", "Минивэн"};
        string[] amountseats = {">5"};
        string[] bagsize = { "Средний", "Большой", "Огромный" };

        Body.Set(body);
        AmountSeats.Set(amountseats);
        BagSize.Set(bagsize);
    }

    private void Style()
    {
        string[] body = { "Седан", "Купе", "Кабриолет"};

        Body.Set(body);
    }

    private void Default()
    { 
        Body.Default();
        AmountSeats.Default();
        BagSize.Default();
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