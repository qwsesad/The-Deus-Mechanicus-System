using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{

    [SerializeField] private Toggle[] toggles;
    [SerializeField] private GameObject Parent;
    [SerializeField] private Text TName;

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
        string output = "";

        for (int i = 0; i < amount; i++)
        {
            if (toggles[i].isOn)
            {
                output += TName.text + " LIKE \'%" + toggles[i].GetComponentInChildren<Text>().text + "%\' OR ";
            }
        }

        if (output == "")
        {
            for (int i = 0; i < amount; i++)
            {
                output += TName.text + " LIKE \'%" + toggles[i].GetComponentInChildren<Text>().text + "%\' OR ";
            }
        }

        char[] tr = { 'O', 'R', ' '};

        output = "(" + output.TrimEnd(tr) + ")";

        return output;
    }

    public void Set(string[] mas)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (string name in mas)
            {
                if (toggles[i].GetComponentInChildren<Text>().text == name)
                {
                    toggles[i].isOn = true;
                }
            }
        }
    }

    public void Default()
    {
        for (int i = 0; i < amount; i++)
        {
            toggles[i].isOn = false;
        }
    }
}
