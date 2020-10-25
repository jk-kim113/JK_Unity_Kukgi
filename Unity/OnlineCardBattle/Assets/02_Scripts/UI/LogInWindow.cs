using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _fieldID;
    [SerializeField]
    InputField _fieldPW;
#pragma warning restore

    public void ClickJoinBtn()
    {
        LogInManager._instace.OpenJoinWnd();
    }

    public void ClickLogInBtn()
    {
        if (!string.IsNullOrEmpty(_fieldID.text) && !string.IsNullOrEmpty(_fieldPW.text))
            ClientManager._instance.LogIn(_fieldID.text, _fieldPW.text);
    }
}
