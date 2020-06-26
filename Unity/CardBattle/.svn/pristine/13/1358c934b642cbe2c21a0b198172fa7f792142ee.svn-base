using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterInfo
{
    public enum eTypeShape
    {
        EFREET      = 0,
        GNOME,
        JINN,
        UNDINE
    }

    public string _name;
    public int _att;
    public int _def;
    public int _maxLife;
    public float _accuracy;
    public float _evasionRate;
    public BaseStatus.eTypeRank _rank;
    public BaseStatus.eTypeCharacter _charac;
    public eTypeShape _shapeType;

    public MonsterInfo(string name, int att, int def, int hp, float acc, float eva, 
                        BaseStatus.eTypeRank rank, BaseStatus.eTypeCharacter ch, eTypeShape shape)
    {
        _name = name;
        _att = att;
        _def = def;
        _maxLife = hp;
        _accuracy = acc;
        _evasionRate = eva;
        _rank = rank;
        _charac = ch;
        _shapeType = shape;
    }
}
