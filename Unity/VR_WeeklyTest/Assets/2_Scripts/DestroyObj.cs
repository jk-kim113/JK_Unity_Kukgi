using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
