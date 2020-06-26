using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    Monster _testMon;

    private void Start()
    {
        _testMon = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            int lMask = 1 << LayerMask.NameToLayer("FIELD");
            
            if (Physics.Raycast(r, out hit, Mathf.Infinity, lMask))
            {
                _testMon.SettingGoalPosition(hit.point);
            }
        }
    }
}
