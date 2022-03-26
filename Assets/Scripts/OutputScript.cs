using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class OutputScript : MonoBehaviour
{
    public AutoCardScript card;

    public bool ready = true;

    public async UniTask Create(string name, string link, string image, string cost)
    {
        var nobj = Instantiate(card, this.transform);
        await nobj.SetUp(name, link, image, cost);
    }

    public void Destroy()
    {
        ready = false;
        GameObject[] Objects;

        Objects = GameObject.FindGameObjectsWithTag("AutoCard");
        foreach (GameObject ob in Objects)
        {
            Destroy(ob);
        }
        ready = true;
    }
}
