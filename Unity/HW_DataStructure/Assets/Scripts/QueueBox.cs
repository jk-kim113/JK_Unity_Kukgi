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

    private void Awake()
    {
        _qData.Enqueue(1);
        _qData.Enqueue(2);
        _qData.Enqueue(3);
        _qData.Enqueue(4);
    }

    private void Start()
    {
        for (int n = 0; n < _textBoxArr.Length; n++)
            _textBoxArr[n].text = (n + 1).ToString();
    }
}
