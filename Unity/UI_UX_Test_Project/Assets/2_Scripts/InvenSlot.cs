using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Sprite _selectImg;
    [SerializeField]
    Sprite _originImg;
#pragma warning restore

    Image _currentImg;
    InvenWindow _owner;
    int _slotID;

    private void Awake()
    {
        _currentImg = GetComponent<Image>();
    }

    public void InitSlot(InvenWindow owner, int slotID)
    {
        _owner = owner;
        _slotID = slotID;
    }

    public void SelectSlot()
    {
        _currentImg.sprite = _selectImg;
        _owner.SelectSlot(_slotID);
    }

    public void NonSelectSlot()
    {
        _currentImg.sprite = _originImg;
    }
}
