using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
    [SerializeField]
    GameObject _rootSlotObj;
    [SerializeField]
    GameObject _wndObj;
    [SerializeField]
    Image _btnUse;
    [SerializeField]
    Color _selectBtnUse;
#pragma warning restore

    InvenSlot[] _arrInvenSlot;
    Vector3 _originSizeBtnExit;

    Color _originBtnUse;

    private void Start()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
        _originBtnUse = _btnUse.color;

        _arrInvenSlot = _rootSlotObj.GetComponentsInChildren<InvenSlot>();

        for (int n = 0; n < _arrInvenSlot.Length; n++)
            _arrInvenSlot[n].InitSlot(this, n);

        _wndObj.SetActive(false);
    }

    public void SelectSlot(int id)
    {   
        for (int n = 0; n < _arrInvenSlot.Length; n++)
        {
            if (n == id)
                continue;
            _arrInvenSlot[n].NonSelectSlot();
        }
    }
    public void OpenWindow()
    {
        _wndObj.SetActive(true);
    }

    public void DownUseButton()
    {
        _btnUse.color = _selectBtnUse;
    }

    public void UpUseButton()
    {
        _btnUse.color = _originBtnUse;
    }

    #region Exit Button Trigger Function
    public void DownExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit * 1.2f;
    }

    public void UpExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit;
        _wndObj.SetActive(false);
    }
    #endregion
}
