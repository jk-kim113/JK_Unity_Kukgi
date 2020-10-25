using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessageWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _systemMessage;
#pragma warning restore

    public void OpenWnd(string msg)
    {
        _systemMessage.text = msg;
    }

    public void CloseWnd()
    {
        Destroy(gameObject);
    }
}
