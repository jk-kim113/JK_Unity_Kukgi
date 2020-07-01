using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldStatusWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Slider _gaugeHP;
    [SerializeField]
    float _limitViewTime = 3.0f;
#pragma warning restore

    float _timeCheck = int.MaxValue;

    private void LateUpdate()
    {
        if(gameObject.activeSelf)
        {
            _timeCheck += Time.deltaTime;
            if(_timeCheck >= _limitViewTime)
                gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // 카메라를 보자
        transform.LookAt(Camera.main.transform);
    }

    public void SettingHPBar(float rate)
    {
        gameObject.SetActive(true);
        _timeCheck = 0;
        _gaugeHP.value = rate;
    }
}
