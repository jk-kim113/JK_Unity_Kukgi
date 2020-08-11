using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _btnItemWnd;
    [SerializeField]
    Color _selectBtnItemWnd;
    [SerializeField]
    GameObject _wndResult;
#pragma warning restore

    Color _originBtnItemWnd;

    InvenWindow _wndInven;

    private void Start()
    {
        _originBtnItemWnd = _btnItemWnd.color;
        _wndInven = GameObject.FindGameObjectWithTag("InvenWindow").GetComponent<InvenWindow>();

        _wndResult.SetActive(false);
    }

    public void UpItemWndButton()
    {
        _btnItemWnd.color = _originBtnItemWnd;
        _wndInven.OpenWindow();
    }

    public void DownItemWndButton()
    {
        _btnItemWnd.color = _selectBtnItemWnd;
    }

    public void OpenResultWnd()
    {
        _wndResult.SetActive(true);
    }
}
