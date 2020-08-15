using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    DownSizeUpButton _btnExit;
    [SerializeField]
    GameObject _wndObj;
#pragma warning restore

    private void Start()
    {
        _btnExit.InitButton(this);
    }

    public override void ExitButton()
    {
        _wndObj.SetActive(false);
    }
}
