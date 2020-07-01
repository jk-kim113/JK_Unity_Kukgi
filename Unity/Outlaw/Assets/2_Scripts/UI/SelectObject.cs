using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    enum eSelectType
    {
        normal      = 0,
        Select,
        Highlight
    }

    

#pragma warning disable 0649
    [SerializeField]
    Material[] _mats;
    [SerializeField]
    LobbyManager.eTypeWindow _windowType;
#pragma warning restore

    MeshRenderer _modelRenderer;

    bool _isIn = false;

    private void Start()
    {
        _modelRenderer = transform.GetComponentInChildren<MeshRenderer>(); // 본인도 포함
    }

    private void OnMouseDown()
    {
        _modelRenderer.material = _mats[(int)eSelectType.Select];
    }

    private void OnMouseUp()
    {
        if (_isIn)
        {
            LobbyManager._instance.OpenWindow(_windowType);
        }

        _modelRenderer.material = _mats[(int)eSelectType.normal];
    }

    private void OnMouseEnter()
    {
        _isIn = true;
        _modelRenderer.material = _mats[(int)eSelectType.Highlight];
    }

    private void OnMouseExit()
    {
        _isIn = false;
        _modelRenderer.material = _mats[(int)eSelectType.normal];
    }
}