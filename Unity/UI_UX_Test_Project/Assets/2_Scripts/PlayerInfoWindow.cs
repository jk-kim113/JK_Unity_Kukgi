using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    DownSizeUpButton _btnExit;
#pragma warning restore

    Animator _animCtrl;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
    }

    private void Start()
    {
        _btnExit.InitButton(this);
    }

    public void UpDownWindow()
    {
        _animCtrl.SetTrigger("Move");
    }

    public override void ExitButton()
    {
        _animCtrl.SetTrigger("Move");
    }
}
