using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeDiffDetector : Singleton<ShapeDiffDetector>
{
    public RenderTexture baseTexture;
    public RenderTexture maskTexture;
    public RawImage debugger;
    int[] _basePixels;

    private void GetBasePixel()
    {
        Color32[] pixels = GetPixelsFromRT(baseTexture);
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
    private Color32[] GetPixelsFromRT(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();
       
        return tex.GetPixels32();
    }
    [ButtonMethod]
    public float DetectDiffPercentage()
    {
        if(_basePixels == null || _basePixels.Length == 0)
        {
            GetBasePixel();
        }

        Color32[] pixels = GetPixelsFromRT(maskTexture);
        float diffCount = 0;
        for (int i = 0; i < _basePixels.Length; i++)
        {
            var pixel = pixels[_basePixels[i]];
            if(pixel.a < 0.5f)
            {

                diffCount++;
            }
        }

        return diffCount /( _basePixels.Length * 1.0f);
    }
}

