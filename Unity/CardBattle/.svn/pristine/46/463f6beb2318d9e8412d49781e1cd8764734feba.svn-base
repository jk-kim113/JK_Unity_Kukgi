using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStatus : MonoBehaviour
{
    public enum eTypeCharacter
    {
        Lazy        = 0,
        Fool,
        Normal,
        Impatient,
        Ferocious
    }

    public enum eTypeRank
    {
        Shallow     = 0,
        Normal,
        Elite,
        RoyalElite,
        Boss
    }

    public enum eTypeStateAction
    {
        IDLE        = 0,
        RUN,
        ATTACK,
        HIT,
        DEAD
    }

    string _name;
    int _att;
    int _def;
    int _maxLife = 1;
    int _nowLife = 1;
    float _accuracy;
    float _evasionRate;

    protected int _currentHP { get { return _nowLife; } set { _nowLife = value; } }

    protected int _attackPow { get { return _att; } }

    protected int _deffencePow { get { return _def; } }

    protected float _evasionPow { get { return _evasionRate; } }

    public float _currentHPRate { get { return (float)_nowLife / _maxLife; } }

    public string _myName { get { return _name; } }

    public abstract int FinalDamage();
    public abstract int FinishDeffence();

    protected void InitStatInfo(string name, int att, int def, int hp, float acc, float evas)
    {
        _name = name;
        _att = att;
        _def = def;
        _maxLife = _nowLife = hp;
        _accuracy = acc;
        _evasionRate = evas;
    }

    // 피격 당했을때의 처리.
    public abstract void OnHitting(int Damage);
}
