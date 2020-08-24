using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSPButton : MonoBehaviour
{
    public enum eRSPType
    {
        Rock,
        Scissor,
        Paper,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    Color _selectColor;
    [SerializeField]
    eRSPType _rspType;
#pragma warning restore

    Image _img;
    Color _originColor;

    private void Awake()
    {
        _img = GetComponent<Image>();
        _originColor = _img.color;
    }

    public void DownBtn()
    {
        _img.color = _selectColor;
    }

    public void UpBtn()
    {
        _img.color = _originColor;
        StageManager._instance.SendRSPType(_rspType);
    }
}
