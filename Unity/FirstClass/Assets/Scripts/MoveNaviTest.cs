using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNaviTest : MonoBehaviour
{
    NavMeshAgent _navMeshAg;

    private void Awake()
    {
        _navMeshAg = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {   
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit))
            {
                _navMeshAg.destination = hit.point;
            }
        }

    }
}
