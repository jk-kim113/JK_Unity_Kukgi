using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtOrderNum;
    [SerializeField]
    Sprite _selectImg;
    [SerializeField]
    Sprite _originImg;
#pragma warning restore

    Image _currentImg;
    SelectStageWindow _owner;
    int _idSlot;

    private void Awake()
    {
        _currentImg = GetComponent<Image>();
    }

    public void InitSlot(SelectStageWindow owner, int id)
    {
        _owner = owner;
        _idSlot = id;
    }

    public void WriteOderNumber(string orderNum)
    {
        _txtOrderNum.text = orderNum;
    }

    public void NonSelectSlot()
    {
        _currentImg.sprite = _originImg;
    }

    public void DownSlotButton()
    {
        _currentImg.sprite = _selectImg;
        _owner.SelectSlot(_idSlot);
    }
}
