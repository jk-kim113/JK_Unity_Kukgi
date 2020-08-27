using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
