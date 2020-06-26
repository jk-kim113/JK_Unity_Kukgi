using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    private float mSpeed = 8;
    private Rigidbody mRB;
    private Vector3 mMoveDistance;
    
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.left * mSpeed;
        mMoveDistance = new Vector3(18 * 2, 0, 0);
    }

    private void Update()
    {
        if(transform.position.x <= - 18)
        {
            transform.position += mMoveDistance;
        }
    }

}
