using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightZone : MonoBehaviour
{
    Monster _owner;

    public void InitSetting(Monster owner)
    {
        _owner = owner;
        SphereCollider sc = GetComponent<SphereCollider>();
        sc.radius = _owner._lengthSight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player p = other.gameObject.GetComponent<Player>();
            _owner.OnBattle(p);
        }
    }
}
