using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{ 
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
    public Toggle2Script Climate;
    public Toggle2Script Roof;
    public ToggleScript Security;
    public ToggleScript Cabin;
    public Toggle2Script Multimedia;
    public Toggle2Script Assist;
    public Toggle2Script Airbags;

    public MinMaxScript Power;
    public MinMaxScript Size;
    public MinMaxScript Acceleration;
    public MinMaxScript Speed;
    public MinMaxScript FuelConsumption;
    public MinMaxScript PowerReserve;
    public MinMaxScript Cost;
    public MinMaxScript RoadP;
    public MinMaxScript Year;

    public Button Next;
    public Button Prev;


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
    public int nel;
    private int ncul;
    private int page;
    private int npage;
    List<Auto> autos = new List<Auto>();

    public OutputScript output;
    public async UniTask GetData()
    {
        var data0 = Get();
        output.Destroy();
        var data = FixString(await data0);
        if (data == "{\"Items\":Товары не найдены}" || data == "{\"Items\":[]}")
        {
            await output.Create("Машины не найдены", "https://ukr.host/kb/wp-content/uploads/2018/05/404.jpg", "https://ukr.host/kb/wp-content/uploads/2018/05/404.jpg", "");
        }
        else if (data == "{\"Items\":Неправильно переданы данные}")
        {
            await output.Create("Ошибка в передаче данных", "https://cleverics.ru/digital/wp-content/uploads/2014/03/error.png", "https://cleverics.ru/digital/wp-content/uploads/2014/03/error.png", "");
        }
        else
        {
            if (autos.Count != 0)
                autos.Clear();
            ncul = 0;
            Auto[] a = JsonHelper.FromJson<Auto>(data);
            ncul = a.Length;
            npage = ncul / nel;
            page = 0;
            foreach (Auto au in a)
            {
                autos.Add(au);
            }
            ButtonChange();
            Create();
        }
    }

    private void Start()
    {
        Screen.SetResolution(1600, 900, false);
    }

    public async void Search()
    {
        bool g = Body.ready && BagSize.ready && AmountSeats.ready && AmountDoors.ready &&Country.ready && Mark.ready && TypeOfDrive.ready &&
                FuelType.ready && ElectricWindows.ready && Climate.ready && Roof.ready && Security.ready && Cabin.ready && Multimedia.ready &&
                Assist.ready && Airbags.ready && Transmission.ready && Power.ready && Size.ready && Acceleration.ready && Speed.ready &&
                FuelConsumption.ready && PowerReserve.ready && Cost.ready && RoadP.ready && Year.ready;
        if (g)
        {
            output.Destroy();
            if (output.ready)
                await GetData();
        }
        else
        {
            await Task.Delay(5);
            Search();
        }
    }

    public string FixString(string data)
    {
        var str = "{\"Items\":" + data + "}";
        return str;
    }

    public void Exit()
    {
        output.Destroy();
        if (output.ready)
        {
            Application.Quit();
        }
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

        Default();
        switch (value)
        {
            case 1:
                Family();
                break;
            case 2:
                Style();
                break;
            case 3:
                LongTrip();
                break;
            case 4:
                Business();
                break;
            case 5:
                ActiveHolidays();
                break;
            case 6:
                SportCars();
                break;
            case 7:
                Dacha();
                break;
            case 8:
                City();
                break;
            default:
                break;
        }
    }

    private void Family()
    {
        string[] body ={ "Седан", "Хетчбэк", "Универсал", "Внедорожник", "Кроссовер", "Минивэн"};
        string[] amountseats = {">5"};
        string[] bagsize = { "Средний", "Большой", "Огромный" };
        string[] airbags = { "Водительские", "Пассажирские", "Боковые", "Шторки" };

        Body.Set(body);
        AmountSeats.Set(amountseats);
        BagSize.Set(bagsize);
        Airbags.Set(airbags);
    }

    private void Style()
    {
        string[] body = { "Седан", "Купе", "Кабриолет"};

        Body.Set(body);
    }

    private void LongTrip()
    {
        string[] body = { "Седан", "Хетчбэк", "Универсал", "Внедорожник", "Кроссовер", "Минивэн" };
        string[] bagsize = { "Средний", "Большой", "Огромный" };

        PowerReserve.SetMin(800);
        Body.Set(body);
        BagSize.Set(bagsize);
    }

    private void ActiveHolidays()
    {
        string[] body = {"Внедорожник", "Кроссовер", "Минивэн", "Пикап" };
        string[] amountseats = { "4", ">5" };
        string[] typeofdrive = { "Полный" };

        RoadP.SetMin(180);
        AmountSeats.Set(amountseats);
        TypeOfDrive.Set(typeofdrive);
        Body.Set(body);
    }

    private void Business()
    {
        string[] body = { "Минивэн", "Микроавтобус", "Пикап", "Фургон", "Шасси", "Борт" };
      
        Body.Set(body);
    }

    private void SportCars()
    {
        string[] body = {"Кроссовер", "Купе", "Кабриолет" };


        Acceleration.SetMax(8);
        Body.Set(body);
    }

    private void Dacha()
    {
        string[] body = { "Внедорожник", "Кроссовер"};
        string[] bagsize = { "Средний", "Большой", "Огромный" };

        Body.Set(body);
        BagSize.Set(bagsize);
    }

    private void City()
    {
        string[] body = { "Седан", "Хетчбэк" };

        Body.Set(body);
    }

    private void Default()
    { 
        Body.Default();
        BagSize.Default();
        AmountSeats.Default();
        AmountDoors.Default();
        Country.Default();
        Mark.Default();
        TypeOfDrive.Default();
        FuelType.Default();
        ElectricWindows.Default();
        Climate.Default();
        Roof.Default();
        Security.Default();
        Cabin.Default();
        Multimedia.Default();
        Assist.Default();
        Airbags.Default();
        Transmission.Default();
        Power.Default();
        Size.Default();
        Acceleration.Default();
        Speed.Default();
        FuelConsumption.Default();
        PowerReserve.Default();
        Cost.Default();
        RoadP.Default();
        Year.Default();
    }

    private void OnDestroy()
    {
        output.Destroy();
    }

    private void ButtonChange()
    {
        if (page >= npage - 1 && page == 0)
        {
            Next.gameObject.SetActive(false);
            Prev.gameObject.SetActive(false);
        }
        else if (page < npage - 1 && page != 0)
        {
            Next.gameObject.SetActive(true);
            Prev.gameObject.SetActive(true);
        }
        else if (page != 0)
        {
            Next.gameObject.SetActive(false);
            Prev.gameObject.SetActive(true);
        }
        else if (page < npage - 1)
        {
            Next.gameObject.SetActive(true);
            Prev.gameObject.SetActive(false);
        }
    }

    public void Plus()
    {
        if (page < npage - 1)
        {
            page++;
            output.Destroy();
            ButtonChange();
            Create();
        }
    }

    public void Minus()
    {
        if (page > 0)
        {
            page--;
            output.Destroy();
            ButtonChange();
            Create();
        }
    }

    public async void Create()
    {
        List<UniTask> tasks = new List<UniTask>();
        for (int i = page*nel; i < nel*(page+1) && i < ncul; i++)
        {
            UniTask t =  output.Create(autos[i].name, autos[i].link, autos[i].image, "от " + autos[i].cost + " руб.");
            tasks.Add(t);
        }
        await UniTask.WhenAll(tasks);
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

