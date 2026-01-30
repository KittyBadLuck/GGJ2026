using MyBox;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectShape : MonoBehaviour
{
    public RenderTexture baseTexture;
    public RenderTexture maskTexture;

    public RawImage testTexture;

    int[] _basePixels;

    public void GetBasePixel()
    {
        Texture2D tex = new Texture2D(baseTexture.width, baseTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = baseTexture;

        tex.ReadPixels(new Rect(0, 0, baseTexture.width, baseTexture.height), 0, 0);
        tex.Apply();
        testTexture.texture = tex;
        Color32[] pixels = tex.GetPixels32();
        List<int> basePixels = new List<int>();
        for (int i = 0; i < pixels.Length; i++)
        {
            if(pixels[i].a > 0.5f)
            {
                basePixels.Add(i);
            }
        }
        _basePixels = basePixels.ToArray();
    }

    [ButtonMethod]
    public void DetectDiff()
    {
        if(_basePixels == null || _basePixels.Length == 0)
        {
            GetBasePixel();
        }
        Texture2D tex = new Texture2D(maskTexture.width, maskTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = maskTexture;

        tex.ReadPixels(new Rect(0, 0, maskTexture.width, maskTexture.height), 0, 0);
        tex.Apply();
        Color32[] pixels = tex.GetPixels32();
        var diffCount = 0;
        for (int i = 0; i < _basePixels.Length; i++)
        {
            var pixel = pixels[_basePixels[i]];
            if(pixel.a < 0.5f)
            {

                diffCount++;
            }
        }

        Debug.Log($"Diff Count: {diffCount} / {_basePixels.Length}");
    }
}

