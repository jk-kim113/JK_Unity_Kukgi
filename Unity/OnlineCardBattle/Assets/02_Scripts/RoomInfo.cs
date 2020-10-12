using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfo : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _titleTxt;
    [SerializeField]
    GameObject _lockImg;
    [SerializeField]
    Text _memberNumTxt;
    [SerializeField]
    Button _enterBtn;
#pragma warning restore

    int _roomNumber;

    public void InitRoom(int roomNum, string title, bool isLock, int currentMember, int maxMember, LobbyManager.VoidCallBack callback)
    {
        _roomNumber = roomNum;
        _titleTxt.text = title;
        _lockImg.SetActive(isLock);
        _memberNumTxt.text = string.Format("{0} / {1}", currentMember, maxMember);
        _enterBtn.onClick.AddListener(() => { callback(_roomNumber, isLock); });
    }
}
