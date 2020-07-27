using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public enum eBoxType
    {
        Queue       = 0,
        Stack
    }

#pragma warning disable 0649
    [SerializeField]
    eBoxType _boxType;
#pragma warning restore

    public eBoxType _nowBoxType { get { return _boxType; } }

    public bool _isEmpty { get { return IsEmpty(); } }

    public bool _isChceck;
    float _timeCheck;
    float _timeInterval = 5.0f;

    private void Awake()
    {
        _isChceck = false;
    }

    private void Update()
    {
        if(_isChceck)
        {
            _timeCheck += Time.deltaTime;
            if(_timeCheck > _timeInterval)
            {
                _timeCheck = 0;
                FillEmpty();
            }
        }
    }

    protected abstract bool IsEmpty();

    protected abstract void FillEmpty();

    public abstract int GetData();

    public abstract void ResetText();

    public void UseButton()
    {
        DataClass._instance.Use(_boxType);
    }

    public void AddButton()
    {
        DataClass._instance.Add(this);
    }

    public void DeleteButton()
    {
        DataClass._instance.Delete(_boxType);
    }
}
