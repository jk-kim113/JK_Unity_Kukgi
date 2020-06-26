using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _force = 800;
    Rigidbody _rgb3D;

    int _myAtt = 3;
    bool _isPlayer = true;
    UnitBase _ownerCharacter;

    public UnitBase _ownerTarget { get { return _ownerCharacter; } }

    public int _finalDamage
    {
        get
        {
            int baseDam = 0;
            if (_isPlayer)
                baseDam = ((Player)_ownerCharacter)._finalDamage;
            else
                baseDam = ((Monster)_ownerCharacter)._finalDamage;
            return _myAtt + baseDam;
        }
    }

    private void Awake()
    {
        _rgb3D = GetComponent<Rigidbody>();
        _rgb3D.AddForce(transform.forward * _force);
        Destroy(gameObject, 3.0f);
    }

    public void InitBulletData(UnitBase owner, bool isPlayer = true)
    {
        _ownerCharacter = owner;
        _isPlayer = isPlayer;
    }
}
