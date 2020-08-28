using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : TSingleton<ResourcePoolManager>
{
    public enum eResourceKind
    {
        Image,
        Prefab,

        max
    }

    Dictionary<string, Sprite> _imageData = new Dictionary<string, Sprite>();
    Dictionary<string, GameObject> _prefabData = new Dictionary<string, GameObject>();

    //Dictionary<string, object> _imageData2 = new Dictionary<string, object>();
    //Dictionary<string, object> _prefabData2 = new Dictionary<string, object>();
    Dictionary<eResourceKind, Dictionary<string, object>> _resourceData = new Dictionary<eResourceKind, Dictionary<string, object>>();

    Dictionary<eResourceKind, object> _r = new Dictionary<eResourceKind, object>();

    protected override void Init()
    {
        base.Init();
        
        //ResourceAllLoad();
    }

    public T GetObj<T>(eResourceKind kind, string name) where T : Component
    {
        if(_resourceData.ContainsKey(kind))
        {
            if (_resourceData[kind].ContainsKey(name))
            {   
                object o = _resourceData[kind][name];
                return (T)Convert.ChangeType(o, typeof(T));
            }
        }   

        return null;
    }

    void LoadImageData(eResourceKind kind)
    {
        Dictionary<string, object> tempDic = new Dictionary<string, object>();

        TableBase imageTable = TableManager._instance.Get(eTableType.ImageData);

        foreach (string key in imageTable._datas.Keys)
        {
            Sprite image = Resources.Load<Sprite>(imageTable._datas[key]["Location"]);
            tempDic.Add(imageTable._datas[key]["ImageName"], image);
        }

        _resourceData.Add(kind, tempDic);
    }

    public void ResourceAllLoad()
    {
        //for (int n = 1; n <= TableManager._instance.Get(eTableType.ImageData).Length; n++)
        //{
        //    if (!_imageData.ContainsKey(TableManager._instance.Get(eTableType.ImageData).ToS(n, "ImageName")))
        //    {
        //        Sprite sp = Resources.Load<Sprite>(TableManager._instance.Get(eTableType.ImageData).ToS(n, "Location"));

        //        _imageData.Add(TableManager._instance.Get(eTableType.ImageData).ToS(n, "ImageName"), sp);
        //    }
        //}

        TableBase imageTable = TableManager._instance.Get(eTableType.ImageData);

        foreach(string key in imageTable._datas.Keys)
        {
            Sprite image = Resources.Load<Sprite>(imageTable._datas[key]["Location"]);
            _imageData.Add(imageTable._datas[key]["ImageName"], image);
        }

        TableBase prefabTable = TableManager._instance.Get(eTableType.PrefabData);

        foreach(string key in prefabTable._datas.Keys)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabTable._datas[key]["Location"]);
            _prefabData.Add(prefabTable._datas[key]["Name"], prefab);
        }
    }

    public Sprite GetImage(string name)
    {
        if (name.CompareTo("0") == 0)
            return null;

        return _imageData[name];
    }

    public GameObject GetPrefab(string name)
    {
        if (_prefabData.ContainsKey(name))
            return _prefabData[name];

        return null;
    }
}
