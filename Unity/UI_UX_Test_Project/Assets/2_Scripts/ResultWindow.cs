using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
    [SerializeField]
    GameObject _wnd;
#pragma warning restore

    Vector3 _originSizeBtnExit;

    private void Start()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
    }

    #region Exit Button Trigger Function
    public void DownExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit * 1.2f;
    }

    public void UpExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit;
        _wnd.SetActive(false);
    }
    #endregion
}
