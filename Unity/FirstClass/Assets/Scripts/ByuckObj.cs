using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByuckObj : MonoBehaviour
{
    public GameObject _prefabImpact;
    public GameObject _prefabWall;

    public Transform _Floorcheck;

    public int _hitNumber = 10;

    Vector3 _floorPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Magic"))
        {   
            Vector3 hitPos = collision.contacts[0].point;

            Destroy(collision.gameObject);

            GameObject impact = Instantiate(_prefabImpact, hitPos, Quaternion.identity);
            Destroy(impact, 1.5f);

            if(--_hitNumber <= 0)
            {
                Instantiate(_prefabWall, _Floorcheck.position, _Floorcheck.rotation);
                Destroy(gameObject);
                //Vector3 pos = new Vector3(transform.position.x, _floorPos.y, transform.position.z);
                //Instantiate(_prefabWall, pos, Quaternion.identity);
            }
        }

        //if(collision.gameObject.CompareTag("Floor"))
        //{
        //    _floorPos = collision.transform.position;
        //    _floorPos = new Vector3(_floorPos.x, _floorPos.y + collision.transform.localScale.y / 2, _floorPos.z);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            Vector3 hitPos = other.transform.position; //contacts[0].point;

            Destroy(other.gameObject);

            GameObject impact = Instantiate(_prefabImpact, hitPos, Quaternion.identity);
            Destroy(impact, 1.5f);

            if (--_hitNumber <= 0)
            {
                Instantiate(_prefabWall, _Floorcheck.position, _Floorcheck.rotation);
                Destroy(gameObject);
            }
        }
    }
}
