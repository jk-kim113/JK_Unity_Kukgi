using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataClass : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text[] _textBoxArr;
#pragma warning restore

    LinkedData<Action> _lData = new LinkedData<Action>();

    static DataClass _uniqueInstance;
    public static DataClass _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;

        InitTextBox();
    }

    public void Use(Action.eBoxType type)
    {
        //TODO 
    }

    public void Add(Action data)
    {
        if (_lData.size >= 3)
            return;

        _lData.AddLast(data);

        RewriteTextBox();
    }

    public void Delete(Action data)
    {
        int idx = 0;
        while(_lData.Get(idx)._nowBoxType != data._nowBoxType)
        {
            idx++;
        }

        _lData.Remove(idx);

        RewriteTextBox();
    }

    void InitTextBox()
    {
        for (int n = 0; n < _textBoxArr.Length; n++)
            _textBoxArr[n].text = "0";
    }

    void RewriteTextBox()
    {
        InitTextBox();

        for (int n = 0; n < _lData.size; n++)
        {
            switch (_lData.Get(n)._nowBoxType)
            {
                case Action.eBoxType.Queue:
                    _textBoxArr[n].text = "1";
                    break;
                case Action.eBoxType.Stack:
                    _textBoxArr[n].text = "2";
                    break;
            }
        }
    }
}
