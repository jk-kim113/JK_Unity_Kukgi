using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    float _rateFire = 1.3f;
#pragma warning restore

    float _timeFire = 0;
    BoxCollider _sightRange;
    Transform _trRoot;
    bool _isDetect;
    PlayerControl _player;

    private void Awake()
    {
        _sightRange = GetComponent<BoxCollider>();
        //_sightRange.enabled = false;
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 10, Color.red);

        //if(Physics.Raycast(transform.position, transform.forward, 10.0f, 1))
        //{
        //    _sightRange.enabled = false;
        //}
        //else
        //{
        //    _sightRange.enabled = true;
        //}

        if(_isDetect)
        {
            if(_player != null)
                _trRoot.LookAt(_player.gameObject.transform);
            if (Time.time >= _timeFire)
            {
                _timeFire = Time.time + _rateFire;

                if(_player != null)
                {
                    _player.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {
            _trRoot.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void InitSetting(Transform root)
    {
        _trRoot = root;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z) 
                                        - transform.position;
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir.normalized, out hit, 10.0f, 1))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Attack!!!");
                    _player = hit.transform.gameObject.GetComponent<PlayerControl>();
                    _isDetect = true;
                }
                else
                {
                    _player = null;
                    _isDetect = false;
                }
            }   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z)
                                        - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir.normalized, out hit, 10.0f, 1))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    if(!_isDetect)
                    {
                        Debug.Log("Attack!!!");
                        _player = hit.transform.gameObject.GetComponent<PlayerControl>();
                        _isDetect = true;
                    }
                }
                else
                {
                    _player = null;
                    _isDetect = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("쉬어");
            _player = null;
            _isDetect = false;
        }
    }
}
