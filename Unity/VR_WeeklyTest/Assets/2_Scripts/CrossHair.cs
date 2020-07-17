using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Image _crossHair;
    
    static bool _isGazzing = false;
    public static bool _isGaze { get { return _isGazzing; } set { _isGazzing = value; } }

    private void Awake()
    {
        _crossHair = GetComponent<Image>();
    }

    private void Update()
    {
        if (_isGazzing)
        {
            _crossHair.color = Color.red;
        }
        else
        {
            _crossHair.color = Color.white;
        }
    }
}
