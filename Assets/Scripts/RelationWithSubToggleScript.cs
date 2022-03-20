using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationWithSubToggleScript : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Toggle Parent;

    void Start()
    {

    }

    public void ChangeChildren()
    {
        if (Parent.isOn)
        {
            foreach(Toggle tog in toggles)
            {
                tog.isOn = true;
            }
        }
        else
        {
            foreach (Toggle tog in toggles)
            {
                tog.isOn = false;
            }
        }
    }
}
