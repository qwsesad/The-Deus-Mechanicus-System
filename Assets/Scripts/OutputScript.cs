using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class OutputScript : MonoBehaviour
{
    public AutoCardScript card;
    public async UniTask Create(string name, string link, string image)
    {
        var nobj = Instantiate(card, this.transform);
        await nobj.SetUp(name, link, image);
    }

    public void Destroy()
    {
        GameObject[] Objects;

        Objects = GameObject.FindGameObjectsWithTag("AutoCard");
        foreach (GameObject ob in Objects)
        {
            Destroy(ob);
        }
    }
}
