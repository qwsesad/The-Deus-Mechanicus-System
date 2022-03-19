using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks;

public class AutoCardScript : MonoBehaviour
{
    private string Link;
    public Image Image;
    public Text Name;

    private async UniTask<Texture2D> ImgLoad(string image, CancellationToken cancellationtoken = default)
    {
        var img = UnityWebRequestTexture.GetTexture(image);
        await img.SendWebRequest().WithCancellation(cancellationtoken);
        return img.result == UnityWebRequest.Result.Success ? DownloadHandlerTexture.GetContent(img) : null;
    }

    public async UniTask SetUp(string name, string link, string image, string cost, CancellationToken cancellationtoken = default)
    {
        Name.text = name + "\n" + cost;
        Link = link;
        var imgtexture = await ImgLoad(image, cancellationtoken);
        Image.sprite = Sprite.Create(imgtexture, new Rect(0, 0, imgtexture.width, imgtexture.height), new Vector2(0, 0));
    }

    public void OpenUrl()
    {
        Application.OpenURL(Link);
    }
}
