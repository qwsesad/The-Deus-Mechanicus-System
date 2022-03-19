using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortScript : MonoBehaviour
{
    [SerializeField] private Dropdown Parent;

    public string Get_Sort()
    {
        string sort = "ORDER BY ";
        int s = Parent.value;

        switch (s)
        {
            case 0:
                sort += "cost";
                break;
            case 1:
                sort += "cost DESC";
                break;
            case 2:
                sort += "power";
                break;
            case 3:
                sort += "power DESC";
                break;
            default:
                break;
        }

        return sort;
    }
}
