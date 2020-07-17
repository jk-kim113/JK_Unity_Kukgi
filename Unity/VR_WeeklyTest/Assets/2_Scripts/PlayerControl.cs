using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum eTypeMove
    {
        WAY_POINT = 0,
        LOOK_AT
    }

#pragma warning disable 0649
    [SerializeField]
    eTypeMove _movType = eTypeMove.WAY_POINT;
    [SerializeField]
    float _movSpeed = 1;
    [SerializeField]
    float _damping = 3;
#pragma warning restore

    Transform _tfCamera;
    CharacterController _cc;

    Transform[] _movPoints;
    int _nextIdx = 1;

    static bool _isStopping = false;
    public static bool _isStop { set { _isStopping = value; } }

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // RootPoints 게임오브젝트 포함 자식의 모든 게임오브젝트의 Transform컴퍼넌트를 추출.
        _movPoints = GameObject.Find("TrackPoints").GetComponentsInChildren<Transform>();
        _tfCamera = Camera.main.transform;
    }

    private void Update()
    {   
        if (_isStopping)
            return;

        switch (_movType)
        {
            case eTypeMove.WAY_POINT:
                MoveWayPoint();
                break;
            case eTypeMove.LOOK_AT:
                MoveLookAt();
                break;
        }
    }

    void MoveLookAt()
    {
        // 캐릭터 카메라가 바라보는 방향.
        Vector3 direction = _tfCamera.TransformDirection(Vector3.forward);
        _cc.SimpleMove(direction * _movSpeed);
    }

    void MoveWayPoint()
    {
        // 현재 위치에서 다음 포인트를 바라보는 벡터 계산.
        Vector3 direction = _movPoints[_nextIdx].position - transform.position;
        // 벡터의 회전각도를 쿼터니언 값으로 산출.
        Quaternion rot = Quaternion.LookRotation(direction);
        // 현재 각도에서 목적 각도까지의 회전처리(부드럽게)
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * _damping);
        // 전진 방향으로의 이동처리.
        transform.Translate(Vector3.forward * Time.deltaTime * _movSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrackPoint"))
        {
            IngameManager._instance.ActOnTrackPoint(_nextIdx);
            _nextIdx = (++_nextIdx >= _movPoints.Length) ? 1 : _nextIdx;
        }
    }
}
