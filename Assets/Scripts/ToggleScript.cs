using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{

    [SerializeField] private Toggle[] toggles;
    [SerializeField] private GameObject Parent;

    private int amount = 0;

    void Start()
    {
        amount = Parent.transform.childCount;

        toggles = new Toggle[amount];

        for (int i = 0; i < amount; i++)
        {
            toggles[i] = Parent.transform.GetChild(i).GetComponent<Toggle>();
        }
    }

    public string Get_Names()
    {
        string output = "[";

        for (int i = 0; i < amount; i++)
        {
            if (toggles[i].isOn)
            {
                output += "\"" + toggles[i].GetComponentInChildren<Text>().text + "\",";
            }
        }

        if (output == "[")
        {
            for (int i = 0; i < amount; i++)
            {
                output += "\"" + toggles[i].GetComponentInChildren<Text>().text + "\",";
            }
        }

        output = output.TrimEnd(',') + "]";

        return output;
    }
}
