using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtMessage;
#pragma warning restore

    public void OpenMessageBox(string msg = "", bool isOpen = false)
    {
        gameObject.SetActive(isOpen);
        _txtMessage.text = msg;
    }
}
