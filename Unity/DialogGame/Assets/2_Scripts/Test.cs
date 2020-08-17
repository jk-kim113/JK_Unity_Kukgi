using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log(JsonManager._instance.GetToStr("Dialog", 37, "Sentences"));
        Debug.Log(JsonManager._instance.GetToStr("Scenario", 20, "StartIndex"));
    }
}
