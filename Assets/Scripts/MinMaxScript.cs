using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MinMaxScript : MonoBehaviour
{
    [SerializeField] private Text TName;
    [SerializeField] private Transform Parent;
    private InputField min;
    private InputField max;

    public MinMax minmax;

    [Serializable]
    public class MinMax
    {
        public string Min;
        public string Max;
    }


    // Start is called before the first frame update
    void Start()
    {
        SetData();

    }

    public string getValues()
    {
        string minValue = minmax.Min;
        string maxValue = minmax.Max;

        if( min.text != "" )
            minValue = min.text; 

        if( max.text != "" )
            maxValue = max.text;

        string output = TName.text + " BETWEEN " + minValue + " AND " + maxValue;

        return output;
    }

    public async UniTask SetData()
    {
        var data0 = Get();

        foreach (Transform child in Parent)
        {
            if (child.name == "MinSize")
            {
                min = child.gameObject.GetComponent<InputField>();
            }
            if (child.name == "MaxSize")
            {
                max = child.gameObject.GetComponent<InputField>();
            }
        }

        var data = FixString(await data0);
        MinMax[] minmax2 = JsonHelper.FromJson<MinMax>(data);
        minmax.Min = minmax2[0].Min;
        minmax.Max = minmax2[0].Max;
        min.placeholder.GetComponent<Text>().text = minmax.Min;
        max.placeholder.GetComponent<Text>().text = minmax.Max;
    }

    private async UniTask<string> Get(CancellationToken cancellationtoken = default)
    {
        WWWForm form = new WWWForm();
        form.AddField("SQL", CreateQuery());
        var www = UnityWebRequest.Post("http://asdasdadsads.ru/AutoScripts/Index.php", form);
        await www.SendWebRequest().WithCancellation(cancellationtoken);
        return www.result == UnityWebRequest.Result.Success ? www.downloadHandler.text : null;
    }

    private string CreateQuery()
    {
        string query = "SELECT MIN(" + TName.text + ") as Min, MAX(" + TName.text + ") as Max FROM auto";
        return query;
    }

    public void Check()
    {
        
        double mintext = min.text != "" ? double.Parse(min.text) : 0;
        double maxtext = max.text != "" ? double.Parse(max.text) : 0;
        double minmin = double.Parse(minmax.Min.Replace('.', ','));
        double maxmax = double.Parse(minmax.Max.Replace('.', ','));

        if (mintext != 0)
        {
            if (mintext < minmin)
            {
                min.text = minmax.Min;
            }
            else if (mintext != 0 && mintext > maxmax)
            {
                min.text = minmax.Max;
            }
        }

        if (maxtext != 0)
        {
            if (maxtext > maxmax)
            {
                max.text = minmax.Max;
            }
            else if (maxtext < minmin)
            {
                max.text = minmax.Min;
            }
        }

        if (min.text != "" && max.text != "")
        {
            if (double.Parse(min.text) > double.Parse(max.text))
            {
                min.text = max.text;
            }
        }

    }

    public string FixString(string data)
    {
        var str = "{\"Items\":" + data + "}";
        return str;
    }

    public void SetMin(double m)
    {
        if (double.Parse(minmax.Min) <= m && double.Parse(minmax.Min) >= m)
        {
            min.text = m.ToString();
        }
    }

    public void SetMax(double m)
    {
        if (double.Parse(minmax.Min) <= m && double.Parse(minmax.Min) >= m)
        {
            min.text = m.ToString();
        }
    }

    public void Default()
    {
        min.text = "";
        max.text = "";
    }
}
