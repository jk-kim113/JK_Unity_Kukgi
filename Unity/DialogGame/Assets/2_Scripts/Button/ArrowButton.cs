using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
#pragma warning restore

    Vector3 _originSize;

    private void Awake()
    {
        _originSize = transform.localScale;
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
