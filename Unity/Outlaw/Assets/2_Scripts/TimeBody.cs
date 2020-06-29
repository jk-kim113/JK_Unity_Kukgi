using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = false;

    List<PointInTime> pointsInTime = new List<PointInTime>();

    GameObject _modelObj;
    Player _player;

    private void Start()
    {
        _modelObj = transform.GetChild(0).gameObject;
        _player = gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.M))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            _modelObj.transform.rotation = pointInTime.rotation;
            _player._curAction = pointInTime.nowAct;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if(pointsInTime.Count > Mathf.Round(10f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, _modelObj.transform.rotation, _player._curAction));
    }

    public void StartRewind()
    {
        isRewinding = true;
        _player._isRewinding = true;
        Time.timeScale = 0.6f;
    }

    public void StopRewind()
    {
        isRewinding = false;
        _player._isRewinding = false;
        Time.timeScale = 1.0f;
    }
}
