using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltControl : MonoBehaviour
{
    public float _force = 5.0f;
    Rigidbody _rgb3D;

    public float _timeStandard = 3.0f;
    //float _timeSave = 0;

    private void Awake()
    {
        _rgb3D = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rgb3D.AddForce(transform.forward * _force);
        Destroy(gameObject, _timeStandard);
    }

    private void Update()
    {
        //_timeSave += Time.deltaTime;
        //if(_timeSave >= _timeStandard)
        //{
        //    _timeSave = 0;
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.transform.name + "에 충돌했어요!");
    //    Destroy(gameObject);
    //}
}
