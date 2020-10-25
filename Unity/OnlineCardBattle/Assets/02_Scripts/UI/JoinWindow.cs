using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _fieldID;
    [SerializeField]
    InputField _fieldPW;
#pragma warning restore

    bool _isJoinable = false;

    public void OverlapCheck()
    {
        if(!string.IsNullOrEmpty(_fieldID.text))
            ClientManager._instance.OverlapCheck_ID(_fieldID.text);
    }

    public void OverlapResult(bool isOverlap)
    {
        _isJoinable = !isOverlap;
    }

    public void JoinGame()
    {
        if (_isJoinable && !string.IsNullOrEmpty(_fieldID.text) && !string.IsNullOrEmpty(_fieldPW.text))
            ClientManager._instance.JoinGame(_fieldID.text, _fieldPW.text);
    }
}
