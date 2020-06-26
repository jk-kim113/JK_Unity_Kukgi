using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldStatusWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Slider _gaugeHP;
#pragma warning restore

    private void FixedUpdate()
    {
        // 카메라를 보자
        transform.LookAt(Camera.main.transform);
    }

    public void SettingHPBar(float rate)
    {
        _gaugeHP.value = rate;
    }
}
