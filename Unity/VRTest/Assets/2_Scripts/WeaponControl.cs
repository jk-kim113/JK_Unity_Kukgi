using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponControl : MonoBehaviour
{
    [SerializeField]
    AudioClip _fireSFX;
    [SerializeField]
    MeshRenderer _muzzleFlash;
    [SerializeField]
    float _rateFire = 0.08f;
    [SerializeField]
    bool _isFire = false;

    float _timeFire = 0;
    AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _muzzleFlash.enabled = false;
    }

    void Update()
    {
        if(_isFire)
        {
            if(Time.time >= _timeFire)
            {
                _timeFire = Time.time + _rateFire;

                StartCoroutine(FireEffect());
            }
        }
    }

    IEnumerator FireEffect()
    {
        _muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        _muzzleFlash.enabled = true;
        _audio.PlayOneShot(_fireSFX);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        _muzzleFlash.enabled = false;
    }

}
