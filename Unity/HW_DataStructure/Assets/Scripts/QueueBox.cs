using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueBox : Action
{
#pragma warning disable 0649
    [SerializeField]
    Text[] _textBoxArr;
#pragma warning restore

    public QueueData<int> _qData = new QueueData<int>();

    int _element = 0;

    private void Awake()
    {
        _qData.Enqueue(++_element);
        _qData.Enqueue(++_element);
        _qData.Enqueue(++_element);
        _qData.Enqueue(++_element);
    }

    private void Start()
    {
        ResetText();
    }

    protected override bool IsEmpty()
    {
        return _qData.isEmpty;
    }

    public override void ResetText()
    {
        for(int n = 0; n < _textBoxArr.Length; n++)
        {
            _textBoxArr[n].gameObject.SetActive(false);
        }

        List<int> inner = new List<int>();
        int size = _qData.size;
        for (int n = 0; n < size; n++)
        {
            inner.Add(_qData.Dequeue());
            _qData.Enqueue(inner[n]);
        }
        
        for (int n = 0; n < inner.Count; n++)
        {
            _textBoxArr[n].text = inner[n].ToString();
            _textBoxArr[n].gameObject.SetActive(true);
        }
    }

    protected override void FillEmpty()
    {
        if (_element >= 4)
            _element = 0;

        _qData.Enqueue(++_element);
        ResetText();

        if (_qData.size >= 4)
            _isChceck = false;
    }

    public override int GetData()
    {
        return _qData.Dequeue();
    }
}
