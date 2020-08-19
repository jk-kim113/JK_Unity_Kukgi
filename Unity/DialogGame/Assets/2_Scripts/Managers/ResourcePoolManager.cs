using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : TSingleton<ResourcePoolManager>
{
    Dictionary<string, Sprite> _imageData = new Dictionary<string, Sprite>();

    protected override void Init()
    {
        base.Init();

        for(int n = 1; n <= TableManager._instance.Get(eTableType.ImageData).Length; n++)
        {
            if(!_imageData.ContainsKey(TableManager._instance.Get(eTableType.ImageData).ToS(n, "ImageName")))
            {
                Sprite sp = Resources.Load<Sprite>(TableManager._instance.Get(eTableType.ImageData).ToS(n, "Location"));

                _imageData.Add(TableManager._instance.Get(eTableType.ImageData).ToS(n, "ImageName"), sp);
            }
        }
    }

    public Sprite GetImage(string name)
    {
        if (name.CompareTo("0") == 0)
            return null;

        return _imageData[name];
    }
}
