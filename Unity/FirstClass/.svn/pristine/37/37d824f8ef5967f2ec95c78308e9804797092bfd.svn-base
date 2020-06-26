using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject[] _targetObjs;

    int index = 4;

    private void Start()
    {
        MoveFrom();
    }

    void MoveFrom()
    {   
        iTween.MoveFrom(_targetObjs[index - 4], iTween.Hash("z", -5.0f, "time", 2.0f, "easetype", iTween.EaseType.linear
            , "oncomplete", "MoveTo", "oncompletetarget", gameObject));
    }

    void MoveTo()
    {
        iTween.MoveTo(_targetObjs[index - 3], iTween.Hash("time", 2.0f, "position", _targetObjs[index - 2].transform.position, 
            "easetype", iTween.EaseType.linear, "oncomplete", "RotateMoveTo", "oncompletetarget", gameObject));
    }

    void RotateMoveTo()
    {
        iTween.RotateTo(_targetObjs[index - 2], new Vector3(-180.0f, 0, 0), 5.0f);
        iTween.MoveTo(_targetObjs[index - 2], iTween.Hash("time", 2.0f, "position", _targetObjs[index - 1].transform.position, 
            "easetype", iTween.EaseType.linear, "oncomplete", "ScaleMoveTo", "oncompletetarget", gameObject));
    }

    void ScaleMoveTo()
    {
        iTween.ScaleTo(_targetObjs[index - 1], new Vector3(5, 5, 5), 5.0f);
        iTween.MoveTo(_targetObjs[index - 1], iTween.Hash("time", 2.0f, "position", _targetObjs[index - 4].transform.position - new Vector3(0, 0, 5), 
            "easetype", iTween.EaseType.linear, "oncomplete", "SetInitState", "oncompletetarget", gameObject));
    }

    void SetInitState()
    {   
        iTween.ScaleTo(_targetObjs[index - 1], iTween.Hash("scale", new Vector3(1, 1, 1), "time", 5.0f, "easetype", iTween.EaseType.linear
            , "oncomplete", "StartAgain", "oncompletetarget", gameObject));
    }

    void StartAgain()
    {
        iTween.MoveTo(_targetObjs[index - 1], iTween.Hash("time", 2.0f, "position", _targetObjs[index - 4].transform.position,
            "easetype", iTween.EaseType.linear, "oncomplete", "MoveTo", "oncompletetarget", gameObject));
        index--;
    }
}
