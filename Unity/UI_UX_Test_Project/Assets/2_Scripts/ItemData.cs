using System.Collections;
using System.Collections.Generic;

public struct ItemData
{
    public enum eTypeItem
    {
        Consume,
        Equip
    }

    public string _name;
    public string _info;
    public int _value;
    public eTypeItem _type;

    public ItemData(string name, string info, int value, eTypeItem type)
    {
        _name = name;
        _info = info;
        _value = value;
        _type = type;
    }
}
