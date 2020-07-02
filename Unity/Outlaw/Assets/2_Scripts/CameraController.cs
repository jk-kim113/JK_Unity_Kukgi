using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Vector3 _offSet = Vector3.zero;
    [SerializeField]
    Vector3 _firstViewOffSet = Vector3.zero;
    [SerializeField]
    float _followSpeed = 2.5f;
    [SerializeField]
    float _rotSpeed = 5f;
#pragma warning restore

    GameObject _playerObj;
    Player _cPlayer;

    Vector3 _goalPosition;

    private void Start()
    {
        // 임시
        //_playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_playerObj != null)
        {
            if(IngameManager._instance._firstView)
            {
                float currYAngle = Mathf.LerpAngle(transform.eulerAngles.y, _playerObj.transform.eulerAngles.y, _rotSpeed * Time.deltaTime);
                Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

                _goalPosition = _playerObj.transform.position - (rot * Vector3.forward * _firstViewOffSet.z)
                                + (Vector3.up * _firstViewOffSet.y);

                transform.position = Vector3.MoveTowards(transform.position, _goalPosition, _followSpeed * Time.deltaTime);
                transform.LookAt(_cPlayer._tfLookPos);
            }
            else
            {
                // 즉각 반응식 카메라 이동
                // transform.position = _playerObj.transform.position + _offSet;

                // 따라가기식 카메라 이동
                Vector3 target = _playerObj.transform.position + _offSet;
                transform.position = Vector3.Lerp(transform.position, target, _followSpeed * Time.deltaTime);
            }
        }
        //else
        //{
        //    _playerObj = GameObject.FindGameObjectWithTag("Player");
        //}
    }

    public void SetPlayer(GameObject p)
    {
        _playerObj = p;
        _cPlayer = p.GetComponent<Player>();
    }
}
