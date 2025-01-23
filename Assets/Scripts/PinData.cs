using UnityEngine;
using SFB;
using System.IO;
using System;

[Serializable]
public class PinData
{
    public string Name;
    public string Text;
    public byte[] ImageData;
    public float x, y;

    public Vector2 Position
    {
        get
        {
            return new Vector2(x, y);
        }
        set
        {
            x = value.x;
            y = value.y;
        }
    }

    public Texture2D LoadImage()
    {
        ExtensionFilter[] extensions = new ExtensionFilter[] { new("Image", "png", "jpeg", "jpg") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        if (paths.Length == 0)
        {
            return null;
        }

        byte[] tmp = File.ReadAllBytes(paths[0]);

        Texture2D result = GetTexture(tmp);

        if (result == null)
        {
            return null;
        }

        ImageData = tmp;
        return result;
    }

    public Texture2D GetTexture(byte[] array = null)
    {
        Texture2D texture = new Texture2D(0, 0);

        if (!ImageConversion.LoadImage(texture, array ?? ImageData))
        {
            return null;
        }

        return texture;
    }
}
