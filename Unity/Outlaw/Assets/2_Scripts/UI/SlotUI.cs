using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _slotIcon;
    [SerializeField]
    Image _imgSelect;
    [SerializeField]
    Image _imgCover;
#pragma warning restore

    BaseWindow _ownerWnd;
    int _no;
    public int _myNumber { get { return _no; } }

    public void DisableSelect()
    {
        _imgSelect.gameObject.SetActive(false);
    }

    public void EnableCover(bool isOn)
    {
        _imgCover.gameObject.SetActive(isOn);
    }

    public void InitIcon(Sprite icon, BaseWindow wnd, int number, bool isClear)
    {
        _ownerWnd = wnd;
        _no = number;
        _slotIcon.sprite = icon;
        DisableSelect();
        EnableCover(isClear);
    }

    public void OnClick()
    {
        if (!_imgSelect.gameObject.activeSelf && !_imgCover.gameObject.activeSelf)
        {
            _imgSelect.gameObject.SetActive(true);
            _ownerWnd.SelectAllCheck(_no);
        }
    }
}
