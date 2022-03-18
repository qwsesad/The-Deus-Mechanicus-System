using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationWithSubToggleScript : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private GameObject Content;
    [SerializeField] private Toggle Parent;


    private int amount = 0;

    void Start()
    {
        amount = Content.transform.childCount - 1;

        toggles = new Toggle[amount];

        for (int i = 0; i < amount; i++)
        {
            toggles[i] = Content.transform.GetChild(i).GetComponent<Toggle>();
        }
    }

    public void ChangeChildren()
    {
        if (Parent.isOn)
        {
            for (int i = 0; i < amount; i++)
            {
                toggles[i].isOn = true;
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                toggles[i].isOn = false;
            }
        }
    }
}
