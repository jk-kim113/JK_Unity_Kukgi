using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    int _duration;
#pragma warning restore

    GameObject _prefabHitEffect;
    GameObject _prefabExplosionEffect;
    GameObject _prefabFire;

    private void Awake()
    {
        _prefabHitEffect = Resources.Load("Prefabs/ParticleEffects/HitEffect") as GameObject;
        _prefabExplosionEffect = Resources.Load("Prefabs/ParticleEffects/ExplosionEffect") as GameObject;
        _prefabFire = Resources.Load("Prefabs/ParticleEffects/FireEffect") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BulletObj"))
        {   
            Destroy(Instantiate(_prefabHitEffect, other.transform.position, Quaternion.identity), 3f);
            Destroy(other.gameObject);

            if (_duration != 999)
            {
                _duration--;
                if (_duration <= 0)
                {   
                    Destroy(Instantiate(_prefabExplosionEffect, transform.position, Quaternion.identity), 3f);
                    Instantiate(_prefabFire, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
}
