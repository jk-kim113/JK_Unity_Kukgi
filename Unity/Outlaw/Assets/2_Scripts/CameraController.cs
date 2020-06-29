using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Vector3 _offSet = Vector3.zero;
    [SerializeField]
    float _followSpeed = 2.5f;
#pragma warning restore

    GameObject _playerObj;

    private void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_playerObj != null)
        {
            // 즉각 반응식 카메라 이동
            // transform.position = _playerObj.transform.position + _offSet;

            // 따라가기식 카메라 이동
            Vector3 target = _playerObj.transform.position + _offSet;

            transform.position = Vector3.Lerp(transform.position, target, _followSpeed * Time.deltaTime);
        }
        else
        {
            _playerObj = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
