using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController3 : MonoBehaviour
{
    public float _movSpeed = 5;
    CharacterController _cc;
    
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 mv = Vector3.zero;

        if(_cc.isGrounded)
        {
            mv = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            mv = (mv.magnitude > 1) ? mv.normalized : mv;
        }

        mv += Physics.gravity;

        _cc.Move(mv * _movSpeed * Time.deltaTime);
    }

}
