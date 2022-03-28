using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationWithSubToggleScript : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Toggle Parent;

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
            bool check = true;
            foreach (Toggle tog in toggles)
            {
                if (!tog.isOn)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                foreach (Toggle tog in toggles)
                {
                    tog.isOn = false;
                }
            }
        }
    }

    public void ChangeParent()
    {
        if(Parent.isOn)
        {
            foreach (Toggle tog in toggles)
            {
                if (!tog.isOn)
                {
                    Parent.isOn = false;
                    break;
                }
            }
        }
        else
        {
            bool check = true;
            foreach (Toggle tog in toggles)
            {
                if (!tog.isOn)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                Parent.isOn = true;
            }
        }
    }
}
