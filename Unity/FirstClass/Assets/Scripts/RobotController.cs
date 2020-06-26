using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float _movSpeed = 5.0f;
    public float _angleSpeed = 120.0f;
    Vector3 _goalPosition;
    Quaternion newQuatern;


    private void Awake()
    {
        _goalPosition = transform.position;
    }

    private void Update()
    {   
        if (Input.GetButtonDown("Fire1"))
        {
            // Generate ray
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(r, out hit))
            {
                _goalPosition = hit.point;
                //transform.LookAt(_goalPosition);
                //transform.rotation = Quaternion.LookRotation(_goalPosition - transform.position);
                
                newQuatern = Quaternion.LookRotation(_goalPosition - transform.position);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _goalPosition, _movSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newQuatern, _angleSpeed * Time.deltaTime);
    }
}
