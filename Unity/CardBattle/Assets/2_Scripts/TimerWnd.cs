using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerWnd : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtSec;
    [SerializeField]
    Text _txtMSec;
#pragma warning restore

    public void SettingTime(float time)
    {
        int sec = (int)time;
        int msec = (int)((time - sec) * 100);
        _txtSec.text = sec.ToString();
        _txtMSec.text = (msec < 10) ? "0" : string.Empty;
        _txtMSec.text += msec.ToString();
    }
}
