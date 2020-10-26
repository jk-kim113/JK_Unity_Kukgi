using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Transform _roomInfoRoot;
#pragma warning restore

    static LobbyManager _uniqueInstance;
    public static LobbyManager _instance { get { return _uniqueInstance; } }

    GameObject _createRoomWndObj;
    GameObject _roomInfoObj;
    GameObject _enterPWWnd;

    EnterPasswordWindow pwWnd;

    public delegate void VoidCallBack(int num, bool isLock);

    List<RoomInfo> _roomList = new List<RoomInfo>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _createRoomWndObj = Resources.Load("Prefabs/CreateRoomWindow") as GameObject;
        _roomInfoObj = Resources.Load("Prefabs/RoomInfo") as GameObject;
        _enterPWWnd = Resources.Load("Prefabs/EnterPasswordWindow") as GameObject;

        ClientManager._instance._IsInLobby = true;
    }

    public void OpenCreateRoomWindow()
    {
        Instantiate(_createRoomWndObj);
    }

    public void ShowRoomInfo(int roomNumber, string roomName, bool isLock, int currentMember)
    {
        for(int n = 0; n < _roomList.Count; n++)
        {
            if (_roomList[n]._RoomNumber == roomNumber)
            {
                _roomList[n].Renew(roomName, isLock, currentMember, 8);
                return;
            }   
        }

        RoomInfo roomInfo = Instantiate(_roomInfoObj, _roomInfoRoot).GetComponent<RoomInfo>();
        roomInfo.InitRoom(roomNumber, roomName, isLock, currentMember, 8, EnterRoom);

        _roomList.Add(roomInfo);
    }

    public void EnterRoom(int num, bool isLock)
    {
        if(isLock)
        {
            pwWnd = Instantiate(_enterPWWnd).GetComponent<EnterPasswordWindow>();
            pwWnd.InitInfo(num);
        }
        else
        {
            ClientManager._instance.EnterRoom(num, "0000");
        }
    }

    public void FailEnterRoom()
    {
        Destroy(pwWnd.gameObject);
    }
}
