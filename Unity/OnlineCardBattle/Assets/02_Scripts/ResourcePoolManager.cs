using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : TSingleton<ResourcePoolManager>
{
    public enum eResourceKind
    {
        Image,

        max
    }

    Dictionary<eResourceKind, Dictionary<string, object>> _resourceData = new Dictionary<eResourceKind, Dictionary<string, object>>();


    protected override void Init()
    {
        base.Init();

        ResourceAllLoad();
    }

    public T GetObj<T>(eResourceKind kind, string name) where T : class
    {
        if (_resourceData.ContainsKey(kind))
        {
            if (_resourceData[kind].ContainsKey(name))
            {
                object obj = _resourceData[kind][name];
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            else
            {
                Debug.LogWarning("Key is not exist / name : " + name);
            }
        }
        else
        {
            Debug.LogWarning("Key is not exist / kind : " + kind.ToString());
        }

        return null;
    }

    public void ResourceAllLoad()
    {
        Sprite[] sp = Resources.LoadAll<Sprite>("Avatar");
        Dictionary<string, object> temp = new Dictionary<string, object>();
        for (int n = 0; n < sp.Length; n++)
            temp.Add("Avatar" + (n + 1), sp[n]);

        _resourceData.Add(eResourceKind.Image, temp);
    }
}
