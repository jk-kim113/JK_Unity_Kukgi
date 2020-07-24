using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackBox : Action
{
#pragma warning disable 0649
    [SerializeField]
    Text[] _textBoxArr;
#pragma warning restore

    StackData<int> _sData = new StackData<int>();

    private void Awake()
    {
        _sData.Push(100);
        _sData.Push(200);
        _sData.Push(300);
    }

    private void Start()
    {
        for (int n = 0; n < _textBoxArr.Length; n++)
            _textBoxArr[n].text = _sData.Pop().ToString();
    }
}
