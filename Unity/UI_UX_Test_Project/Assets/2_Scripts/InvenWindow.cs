using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _rootSlotObj;
    [SerializeField]
    GameObject _wndObj;
    [SerializeField]
    Image _btnUse;
    [SerializeField]
    Color _selectBtnUse;
    [SerializeField]
    DownSizeUpButton _btnExit;
#pragma warning restore

    InvenSlot[] _arrInvenSlot;

    Color _originBtnUse;

    private void Start()
    {   
        _originBtnUse = _btnUse.color;

        _arrInvenSlot = _rootSlotObj.GetComponentsInChildren<InvenSlot>();

        for (int n = 0; n < _arrInvenSlot.Length; n++)
            _arrInvenSlot[n].InitSlot(this, n);

        _wndObj.SetActive(false);
        _btnExit.InitButton(this);
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

    public override void ExitButton()
    {
        _wndObj.SetActive(false);
    }
}
