using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTableType
{
    Dialog,
    Scenario,
    ImageData,

    max
}

public class TableManager : TSingleton<TableManager>
{
    Dictionary<eTableType, TableBase> _tableData = new Dictionary<eTableType, TableBase>();

    protected override void Init()
    {
        base.Init();
    }

    TableBase Load<T>(eTableType type) where T : TableBase, new()
    {
        if(_tableData.ContainsKey(type))
            return _tableData[type];

        
        TextAsset tAsset = Resources.Load("bin/" + type.ToString()) as TextAsset;
        if(tAsset != null)
        {
            T t = new T();
            t.LoadTable(tAsset.text);
            _tableData.Add(type, t);

            return _tableData[type];
        }

        return null;
    }

    public void LoadAll()
    {
        Load<Dialog>(eTableType.Dialog);
        Load<Scenario>(eTableType.Scenario);
        Load<ImageData>(eTableType.ImageData);
    }

    public TableBase Get(eTableType type)
    {
        if(_tableData.ContainsKey(type))
            return _tableData[type];

        return null;
    }

    public void AllClear()
    {
        _tableData.Clear();
    }
}
