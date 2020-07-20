using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            IngameManager._instance._deathNum++;
            Destroy(other.gameObject);
        }
    }
}
