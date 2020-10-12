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

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _createRoomWndObj = Resources.Load("Prefabs/CreateRoomWindow") as GameObject;
        _roomInfoObj = Resources.Load("Prefabs/RoomInfo") as GameObject;
        _enterPWWnd = Resources.Load("Prefabs/EnterPasswordWindow") as GameObject;
    }

    public void OpenCreateRoomWindow()
    {
        Instantiate(_createRoomWndObj);
    }

    public void ShowRoomInfo(int roomNumber, string roomName, bool isLock, int currentMember)
    {
        RoomInfo roomInfo = Instantiate(_roomInfoObj, _roomInfoRoot).GetComponent<RoomInfo>();
        roomInfo.InitRoom(roomNumber, roomName, isLock, currentMember, 3, EnterRoom);
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
