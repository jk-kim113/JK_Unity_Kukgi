using UnityEngine;

public struct AvatarInfo
{
    string _name;
    int _life;
    int _attack;
    int _defence;
    int _maxBulletNum;

    public AvatarInfo(string name, int life, int att, int def, int maxBullet)
    {
        _name = name;
        _life = life;
        _attack = att;
        _defence = def;
        _maxBulletNum = maxBullet;
    }
}
