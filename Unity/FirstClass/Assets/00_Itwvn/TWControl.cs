using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWControl : MonoBehaviour
{
    private void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("x", -3.0f, "y", -2.0f, "z", -5.0f, "time", 6.0f, "easetype", iTween.EaseType.easeInOutElastic
            ,"delay", 6.0f, "oncomplete", "MoveTo", "oncompletetarget", gameObject));
    }

    void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 5.0f, "z", 3.0f, "time", 7.0f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "MoveBy"
            , "oncompletetarget", gameObject));
    }

    void MoveBy()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", 5.0f, "y", -5.0f, "time", 2.0f, "delay", 3.0f));
    }
}
