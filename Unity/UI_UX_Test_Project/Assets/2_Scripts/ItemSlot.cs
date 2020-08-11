using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Sprite _selectImg;
    [SerializeField]
    Sprite _originImg;
    [SerializeField]
    Image _itemImg;
#pragma warning restore

    ShopWindow _owner;
    Image _currentImg;
    int _itemID;
    public int _ItemIndex { get { return _itemID; } }
    int _slotID;

    private void Awake()
    {
        _currentImg = GetComponent<Image>();
    }

    public void InitSlot(ShopWindow owner, int id)
    {
        _owner = owner;
        _slotID = id;
        _itemImg.gameObject.SetActive(false);
    }

    public void InitSlot()
    {
        _itemImg.gameObject.SetActive(false);
        NonSelectSlot();
    }

    public void InsetItemData(Sprite sprite, int itemID)
    {
        _itemImg.gameObject.SetActive(true);

        _itemImg.sprite = sprite;
        _itemID = itemID;
    }

    public void NonSelectSlot()
    {
        _currentImg.sprite = _originImg;
    }

    public void DownButton()
    {
        _currentImg.sprite = _selectImg;
        _owner.ShowSelectItem(_itemID);
        _owner.SelectSlot(_slotID);
    }
}
