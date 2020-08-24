using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public enum eArrowType
    {
        Right,
        Left,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    eArrowType _arrowType;
    [SerializeField]
    float _downSize = 1.3f;
    [SerializeField]
    Color _offColor;
#pragma warning restore

    Vector3 _originSize;
    Image _img;
    Color _originColor;

    private void Awake()
    {
        _originSize = transform.localScale;
        _img = GetComponent<Image>();
        _originColor = _img.color;
    }

    public void OnButton()
    {
        _img.raycastTarget = true;
        _img.color = _originColor;
    }

    public void OffButton()
    {
        _img.raycastTarget = false;
        _img.color = _offColor;
    }

    public void DownBtn()
    {
        transform.localScale *= _downSize;
    }

    public void UpBtn()
    {
        transform.localScale = _originSize;
        LobyUIManager._instance.MovePage(_arrowType);
    }
}
