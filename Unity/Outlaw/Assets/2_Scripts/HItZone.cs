using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    bool _isMonster;
    UnitBase _ownerCharacter;
    Player _oldPlayer;

    public void InitSetting(UnitBase owner, bool isMon = true)
    {
        _ownerCharacter = owner;
        _isMonster = isMon;
    }

    public void EnableTrigger(bool isOn)
    {
        GetComponent<BoxCollider>().enabled = isOn;
        if (!isOn)
            _oldPlayer = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EnableTrigger(false);
            Player p = other.GetComponent<Player>();
            if (_oldPlayer == p)
                return;
            _oldPlayer = p;
            // isMon에 따른 함수 호출.
            if (_isMonster)
            {
                if (p.OnHitting(((Monster)_ownerCharacter)._finalDamage))
                    ((Monster)_ownerCharacter).Winner();
            }   
            else
            {
                if(p.OnHitting(((Player)_ownerCharacter)._finalDamage))
                {

                }
            }
                

        }
    }
}
