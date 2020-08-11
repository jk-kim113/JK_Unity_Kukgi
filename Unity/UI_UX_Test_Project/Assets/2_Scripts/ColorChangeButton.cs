using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Color _selectColor;
#pragma warning restore

    Image _btn;
    Color _originColor;

    private void Awake()
    {
        _btn = GetComponent<Image>();
    }

    private void Start()
    {
        _originColor = _btn.color;
    }

    public void DownButton()
    {
        _btn.color = _selectColor;
    }

    public void UpButton()
    {
        _btn.color = _originColor;
    }
}
