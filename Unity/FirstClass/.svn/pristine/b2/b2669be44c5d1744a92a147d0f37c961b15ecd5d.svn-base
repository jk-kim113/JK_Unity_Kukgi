using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject _Effect;
    public GameObject _Blast;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Magic"))
        {
            Instantiate(_Blast, transform.position, _Blast.transform.rotation);
            Destroy(gameObject);
            Instantiate(_Effect, transform.position, _Effect.transform.rotation);
        }
    }
}
