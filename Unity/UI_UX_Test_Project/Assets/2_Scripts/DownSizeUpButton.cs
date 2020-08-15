using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSizeUpButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
#pragma warning restore

    BaseWindow _owner;
    Vector3 _originSizeBtnExit;

    private void Awake()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
    }

    public void InitButton(BaseWindow owner)
    {
        _owner = owner;
    }

    public void DownExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit * 1.2f;
    }

    public void UpExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit;
        _owner.ExitButton();
    }
}
