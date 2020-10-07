using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _inputName;
#pragma warning restore

    public void ClickButton()
    {
        if (!string.IsNullOrEmpty(_inputName.text))
            ClientManager._instance.SelectName(_inputName.text);
    }
}
