using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject _RotationObj;

    public Transform _FireTr;

    public GameObject _prefabMagic;

    public float _movSpeed = 5.0f;
    public float _angleSpeed = 120.0f;


    private void Update()
    {
        float rx = Input.GetAxis("Horizontal");
        float mz = Input.GetAxis("Vertical");

        float mx = Input.GetAxis("HorizontalMove");

        transform.Translate(Vector3.forward * mz * Time.deltaTime * _movSpeed);
        _RotationObj.transform.Rotate(Vector3.up * rx * Time.deltaTime * _angleSpeed);
        transform.Translate(Vector3.right * mx * Time.deltaTime * _movSpeed);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_prefabMagic, _FireTr.position, _FireTr.rotation);
        }
    }
}
