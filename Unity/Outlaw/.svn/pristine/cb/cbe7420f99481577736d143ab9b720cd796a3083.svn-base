using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public enum eAniType
    {
        IDLE       = 0,
        RUN,
        WALK_BACK,
        WALK_LEFT,
        WALK_RIGHT,
        ATTACK,
        RELOAD,
        WALK,
        BACKHOME,
        DEAD
    }

    bool _isDie;
    protected bool _isDead { get { return _isDie; } set { _isDie = value;} }

    string _name;
    int _life;
    int _att;
    int _def;
    int _curLife;

    protected string _myName { get { return _name; } }
    protected int _baseAtt { get { return _att; } }
    protected float _hpRate { get { return ((float)_curLife / _life); } }
    protected float _shieldRate { get; }

    protected void InitializeData(string name, int hp, int att, int def)
    {
        this._name = name;
        this._life = this._curLife = hp;
        this._att = att;
        this._def = def;
    }

    protected bool HittingMe(int dam)
    {
        int finishD = dam - _def;
        if (finishD < 1)
            finishD = 1;

        _curLife -= finishD;
        if (_curLife <= 0)
        {
            _curLife = 0;
            return true;
        }
        
        return false;
    }
}
