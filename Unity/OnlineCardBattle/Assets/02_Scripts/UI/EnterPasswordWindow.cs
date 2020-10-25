using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterPasswordWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _passwordIF;
#pragma warning restore

    int _roomNumber;

    public void InitInfo(int roomNum)
    {
        _roomNumber = roomNum;
    }

    public void OKBtn()
    {
        ClientManager._instance.EnterRoom(_roomNumber, _passwordIF.text);
    }

    public void ExitBtn()
    {
        Destroy(gameObject);
    }
}
