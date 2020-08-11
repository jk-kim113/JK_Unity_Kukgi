using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
#pragma warning restore

    Animator _animCtrl;

    Vector3 _originSizeBtnExit;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
    }

    private void Start()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
    }

    public void UpDownWindow()
    {
        _animCtrl.SetTrigger("Move");
    }

    #region Exit Button Trigger Function
    public void DownExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit * 1.2f;
    }

    public void UpExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit;
        _animCtrl.SetTrigger("Move");
    }
    #endregion
}
