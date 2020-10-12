using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
#pragma warning disable 0649
    [SerializeField]
    Image _characImg;
#pragma warning restore

    int _idx;
    Image _edgeImg;
    bool _isSelect = false;

    private void Start()
    {
        _edgeImg = GetComponent<Image>();
    }

    public void InitSlot(Sprite img, int idx)
    {
        _characImg.sprite = img;
        _idx = idx;
    }

    public void OffSelect()
    {
        _edgeImg.color = Color.white;
        _isSelect = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_isSelect)
            _edgeImg.color = Color.red;
        else
            _edgeImg.color = Color.white;

        _isSelect = !_isSelect;
        LogInManager._instace.SelectedCharac(_idx, _isSelect);
    }
}
