using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
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
        DataClass._instance.Delete(this);
    }
}
