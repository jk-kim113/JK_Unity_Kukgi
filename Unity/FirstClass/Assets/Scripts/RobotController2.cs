using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController2 : MonoBehaviour
{
    public float _movSpeed = 5.0f;
    public float _angleSpeed = 120.0f;

    private void Update()
    {
        float rx = Input.GetAxis("Horizontal");
        float mz = Input.GetAxis("Vertical");

        float mx = Input.GetAxis("HorizontalMove");

        //transform.position += (transform.forward * mz * Time.deltaTime * _movSpeed) + (transform.right * mx * Time.deltaTime * _movSpeed);
        //transform.Translate(Vector3.forward * mz * Time.deltaTime * _movSpeed + Vector3.right * mx * Time.deltaTime * _movSpeed);
        //Vector3 mv = new Vector3(mx, 0, mz);
        //mv = (mv.magnitude > 1.0f) ? mv.normalized : mv;
        //transform.Translate(mv * Time.deltaTime * _movSpeed);

        transform.Translate(Vector3.forward * mz * Time.deltaTime * _movSpeed);
        transform.Rotate(Vector3.up * rx * Time.deltaTime * _angleSpeed);
        transform.Translate(Vector3.right * mx * Time.deltaTime * _movSpeed);
    }
}
