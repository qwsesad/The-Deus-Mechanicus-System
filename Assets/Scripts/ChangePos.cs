using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePos : MonoBehaviour
{
    [SerializeField] private GameObject Parent;

    public void ChangeIn()
    {
        Parent.transform.position = new Vector3(0, -62.2f, 0);
    }

    public void ChangeOut()
    {
        Parent.transform.position = new Vector3(-2500, -62.2f, 0);
    }
}
