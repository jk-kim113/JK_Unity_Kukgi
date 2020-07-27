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

    public StackData<int> _sData = new StackData<int>();

    int _element = 0;

    private void Awake()
    {
        _sData.Push(400 - 100 * ++_element);
        _sData.Push(400 - 100 * ++_element);
        _sData.Push(400 - 100 * ++_element);
    }

    private void Start()
    {
        ResetText();
    }

    protected override bool IsEmpty()
    {
        return _sData.isEmpty;
    }

    public override void ResetText()
    {   
        for (int n = 0; n < _textBoxArr.Length; n++)
        {
            _textBoxArr[n].gameObject.SetActive(false);
        }

        List<int> inner = new List<int>();
        int size = _sData.size;
        for (int n = 0; n < size; n++)
        {
            inner.Add(_sData.Pop());
        }

        for(int n = inner.Count - 1; n >= 0; n--)
        {
            _sData.Push(inner[n]);
        }

        for (int n = 0; n < inner.Count; n++)
        {
            _textBoxArr[n].text = inner[n].ToString();
            _textBoxArr[n].gameObject.SetActive(true);
        }
    }

    protected override void FillEmpty()
    {
        if(_element < 0)
            _element = 0;

        _sData.Push(400 - 100 * ++_element);
        ResetText();

        if (_sData.size >= 3)
        {
            _isChceck = false;
            _element = 0;
        }
    }

    public override int GetData()
    {
        return _sData.Pop();
    }

    public override void StartCheck()
    {
        _isChceck = true;
        _element = 0;
    }
}
