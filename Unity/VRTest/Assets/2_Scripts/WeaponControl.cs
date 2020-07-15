using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    AudioClip _fireSFX;
    [SerializeField]
    MeshRenderer _muzzleFlash;
    [SerializeField]
    Transform _bulletTrail;
    [SerializeField]
    float _rateFire = 0.08f;
    [SerializeField]
    bool _isFire = false;
#pragma warning restore

    float _timeFire = 0;
    AudioSource _audio;
    Animator _animWeapon;
    Vector3 _originPos;         // 총의 최초 위치
    RaycastHit _hit;
    Transform _tfCam;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _animWeapon = GetComponent<Animator>();
        _originPos = transform.localPosition;
    }

    private void Start()
    {
        _muzzleFlash.enabled = false;
        _tfCam = Camera.main.transform;
    }

    void Update()
    {
        int layMask = 1 << LayerMask.NameToLayer("Enemy");
        if(Physics.Raycast(_tfCam.position, _tfCam.forward, out _hit, 100.0f, layMask))
        {
            _isFire = true;
        }
        else
        {
            _isFire = false;
        }

        if(_isFire)
        {
            if(Time.time >= _timeFire)
            {
                _timeFire = Time.time + _rateFire;

                _hit.transform.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);

                StartCoroutine(FireEffect());
            }

            float z = Random.Range(1.0f, 6.0f);
            _bulletTrail.localScale = new Vector3(_bulletTrail.localScale.x, _bulletTrail.localScale.y, z);
        }
        else
        {
            _bulletTrail.localScale = new Vector3(_bulletTrail.localScale.x, _bulletTrail.localScale.y, 0);
        }
    }

    IEnumerator FireEffect()
    {
        _animWeapon.enabled = false;
        Vector2 _v2Rd = Random.insideUnitCircle;
        transform.localPosition += new Vector3(0, _v2Rd.x * 0.01f, _v2Rd.y * 0.01f);

        _muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        _muzzleFlash.enabled = true;
        _audio.PlayOneShot(_fireSFX);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        _muzzleFlash.enabled = false;

        transform.localPosition = _originPos;
        _animWeapon.enabled = true;
    }
}
