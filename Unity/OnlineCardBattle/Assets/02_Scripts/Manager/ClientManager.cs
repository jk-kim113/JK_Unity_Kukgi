using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class ClientManager : TSingleton<ClientManager>
{
    const string _ip = "127.0.0.1";
    const int _port = 80;

    Socket _server;

    bool _isConnect = false;

    bool _isIngame = false;
    public bool _IsIngame { set { _isIngame = value; } }

    bool _isInLobby = false;
    public bool _IsInLobby { set { _isInLobby = value; } }

    long _myUUID;
    string _myName;
    int _myAvatar;
    int _myRoomNum;

    public string _MyName { get { return _myName; } }
    public int _MyAvatar { get { return _myAvatar; } }

    Queue<DefinedStructure.PacketInfo> _toClientQueue = new Queue<DefinedStructure.PacketInfo>();
    Queue<byte[]> _fromClientQueue = new Queue<byte[]>();

    protected override void Init()
    {
        base.Init();

        StartCoroutine(AddOrder());
        StartCoroutine(DoOrder());
        StartCoroutine(SendOrder());
    }

    bool Connect(string ipAddress, int port)
    {
        try
        {
            if (!_isConnect)
            {
                // make socket
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _server.Connect(ipAddress, port);

                return true;
            }
        }
        catch (System.Exception ex)
        {
            // 메세지 창에 띄운다.
            Debug.Log(ex.Message);
        }

        return false;
    }

    void ToPacket(DefinedProtocol.eFromClient fromClientID, object str)
    {
        DefinedStructure.PacketInfo packetRecieve1;
        packetRecieve1._id = (int)fromClientID;
        packetRecieve1._data = ConvertPacket.StructureToByteArray(str);
        packetRecieve1._totalSize = packetRecieve1._data.Length;

        _fromClientQueue.Enqueue(ConvertPacket.StructureToByteArray(packetRecieve1));
    }

    IEnumerator AddOrder()
    {
        while(true)
        {
            if (_isConnect && _server != null && _server.Poll(0, SelectMode.SelectRead))
            {
                byte[] buffer = new byte[1032];
                int recvLen = _server.Receive(buffer);
                if (recvLen > 0)
                {
                    DefinedStructure.PacketInfo pToClient = new DefinedStructure.PacketInfo();
                    pToClient = (DefinedStructure.PacketInfo)ConvertPacket.ByteArrayToStructure(buffer, pToClient.GetType(), recvLen);

                    _toClientQueue.Enqueue(pToClient);
                }
            }
            
            yield return null;
        }
    }

    IEnumerator DoOrder()
    {
        while(true)
        {
            if(_toClientQueue.Count != 0)
            {
                DefinedStructure.PacketInfo pToClient = _toClientQueue.Dequeue();

                switch((DefinedProtocol.eToClient)pToClient._id)
                {
                    #region 유저 연결 확인 및 UUID 받는 부분
                    case DefinedProtocol.eToClient.OverlapCheckResult_ID:

                        DefinedStructure.Packet_OverlapCheckResultID pOverlapResult = new DefinedStructure.Packet_OverlapCheckResultID();
                        pOverlapResult = (DefinedStructure.Packet_OverlapCheckResultID)ConvertPacket.ByteArrayToStructure(pToClient._data, pOverlapResult.GetType(), pToClient._totalSize);

                        LogInManager._instace.OverlapReslut_ID(pOverlapResult._result == 0);

                        break;

                    case DefinedProtocol.eToClient.CompleteJoin:

                        DefinedStructure.Packet_CompleteJoin pCompleteJoin = new DefinedStructure.Packet_CompleteJoin();
                        pCompleteJoin = (DefinedStructure.Packet_CompleteJoin)ConvertPacket.ByteArrayToStructure(pToClient._data, pCompleteJoin.GetType(), pToClient._totalSize);

                        _myUUID = pCompleteJoin._UUID;

                        LogInManager._instace.CompleteJoin();

                        break;

                    case DefinedProtocol.eToClient.LogInResult:

                        DefinedStructure.Packet_LogInResult pLogInResult = new DefinedStructure.Packet_LogInResult();
                        pLogInResult = (DefinedStructure.Packet_LogInResult)ConvertPacket.ByteArrayToStructure(pToClient._data, pLogInResult.GetType(), pToClient._totalSize);

                        if(pLogInResult._isSuccess == 0)
                        {
                            _myUUID = pLogInResult._UUID;

                            if(pLogInResult._isFirst != 0)
                            {
                                _myName = pLogInResult._name;
                                _myAvatar = pLogInResult._avatarIndex;
                            }
                        }

                        LogInManager._instace.LogInResult(pLogInResult._isSuccess == 0, pLogInResult._isFirst == 0);

                        break;
                    #endregion

                    #region 방 입장 관련 부분
                    case DefinedProtocol.eToClient.SuccessEnterRoom:

                        SceneManager.LoadScene("IngameScene");

                        while (!_isIngame)
                        {
                            yield return null;
                        }

                        DefinedStructure.Packet_SuccessEnterRoom pSuccessEnter = new DefinedStructure.Packet_SuccessEnterRoom();
                        pSuccessEnter = (DefinedStructure.Packet_SuccessEnterRoom)ConvertPacket.ByteArrayToStructure(pToClient._data, pSuccessEnter.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowUserInfo(pSuccessEnter._slotIndex, _myName, _myAvatar, true);

                        break;

                    case DefinedProtocol.eToClient.FailEnterRoom:

                        LobbyManager._instance.FailEnterRoom();

                        break;

                    case DefinedProtocol.eToClient.AfterCreateRoom:

                        DefinedStructure.Packet_AfterCreateRoom pAfterCreateRoom = new DefinedStructure.Packet_AfterCreateRoom();
                        pAfterCreateRoom = (DefinedStructure.Packet_AfterCreateRoom)ConvertPacket.ByteArrayToStructure(pToClient._data, pAfterCreateRoom.GetType(), pToClient._totalSize);

                        _myRoomNum = pAfterCreateRoom._roomNumber;

                        while (!_isIngame)
                        {
                            yield return null;
                        }

                        IngameManager._instance.ShowMaster(_myName, true);
                        IngameManager._instance.ShowUserInfo(0, _myName, _myAvatar, true);

                        break;

                    case DefinedProtocol.eToClient.ShowRoomInfo:

                        DefinedStructure.Packet_ShowRoomInfo pShowRoomInfo = new DefinedStructure.Packet_ShowRoomInfo();
                        pShowRoomInfo = (DefinedStructure.Packet_ShowRoomInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowRoomInfo.GetType(), pToClient._totalSize);

                        if(_isInLobby)
                            LobbyManager._instance.ShowRoomInfo(pShowRoomInfo._roomNumber, pShowRoomInfo._roomName, pShowRoomInfo._isLock == 0 ? true : false, pShowRoomInfo._currentMemberNum);

                        break;

                    case DefinedProtocol.eToClient.ShowExit:

                        DefinedStructure.Packet_ShowExit pShowExit = new DefinedStructure.Packet_ShowExit();
                        pShowExit = (DefinedStructure.Packet_ShowExit)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowExit.GetType(), pToClient._totalSize);

                        if(!pShowExit._name.Equals(_myName))
                            IngameManager._instance.ShowExit(pShowExit._name);

                        break;

                    #endregion

                    #region 접속시 타 유저에게 내정보를 알려주고 타 유저의 정보를 받아오는 곳
                    case DefinedProtocol.eToClient.ShowMaster:

                        DefinedStructure.Packet_ShowMaster pShowMaster = new DefinedStructure.Packet_ShowMaster();
                        pShowMaster = (DefinedStructure.Packet_ShowMaster)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowMaster.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowMaster(pShowMaster._name, pShowMaster._name == _myName);

                        break;

                    case DefinedProtocol.eToClient.ShowUserInfo:

                        while(!_isIngame)
                        {
                            yield return null;
                        }

                        DefinedStructure.Packet_MyInfo pShowUser = new DefinedStructure.Packet_MyInfo();
                        pShowUser = (DefinedStructure.Packet_MyInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowUser.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowUserInfo(pShowUser._slotIndex, pShowUser._name, pShowUser._avatarIndex, pShowUser._name.Equals(_myName));

                        break;

                    case DefinedProtocol.eToClient.ShowAI:

                        DefinedStructure.Packet_ShowAI pShowAI = new DefinedStructure.Packet_ShowAI();
                        pShowAI = (DefinedStructure.Packet_ShowAI)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowAI.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowAI(pShowAI._slotIndex, pShowAI._aiName);

                        break;
                    #endregion

                    #region 게임 시작을 알려주는 곳
                    case DefinedProtocol.eToClient.ShowReady:

                        DefinedStructure.Packet_ShowReady pShowReady = new DefinedStructure.Packet_ShowReady();
                        pShowReady = (DefinedStructure.Packet_ShowReady)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowReady.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowReady(pShowReady._slotIndex);

                        break;

                    case DefinedProtocol.eToClient.CanPlay:

                        IngameManager._instance.CanPlay();

                        break;

                    case DefinedProtocol.eToClient.GameStart:

                        IngameManager._instance.GameStart();

                        break;
                    #endregion

                    #region 턴 정보를 받아오는 곳
                    case DefinedProtocol.eToClient.NextTurn:

                        DefinedStructure.Packet_NextTurn pNextTurn = new DefinedStructure.Packet_NextTurn();
                        pNextTurn = (DefinedStructure.Packet_NextTurn)ConvertPacket.ByteArrayToStructure(pToClient._data, pNextTurn.GetType(), pToClient._totalSize);

                        while (IngameManager._instance._IsTurnCard)
                            yield return null;

                        IngameManager._instance.ShowTurn(pNextTurn._name, _myName == pNextTurn._name);

                        break;
                    #endregion

                    #region 카드 선택 정보 및 처리
                    case DefinedProtocol.eToClient.ChooseInfo:

                        DefinedStructure.Packet_ChooseInfo pChooseInfo = new DefinedStructure.Packet_ChooseInfo();
                        pChooseInfo = (DefinedStructure.Packet_ChooseInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pChooseInfo.GetType(), pToClient._totalSize);

                        while (IngameManager._instance._IsTurnCard)
                            yield return null;

                        IngameManager._instance.ReverseCard(pChooseInfo._cardIdx1, pChooseInfo._cardIdx2, pChooseInfo._cardImgIdx1, pChooseInfo._cardImgIdx2);

                        break;

                    case DefinedProtocol.eToClient.ChooseResult:

                        DefinedStructure.Packet_ChooseResult pChooseResult = new DefinedStructure.Packet_ChooseResult();
                        pChooseResult = (DefinedStructure.Packet_ChooseResult)ConvertPacket.ByteArrayToStructure(pToClient._data, pChooseResult.GetType(), pToClient._totalSize);

                        if (pChooseResult._isSuccess == 0)
                        {
                            IngameManager._instance.CorrectCard(pChooseResult._name, pChooseResult._cardIdx1, pChooseResult._cardIdx2, pChooseResult._name == _myName);
                        }
                        else
                        {
                            IngameManager._instance.InCorrectCard(pChooseResult._cardIdx1, pChooseResult._cardIdx2);
                        }

                        break;
                    #endregion

                    #region 게임 결과
                    case DefinedProtocol.eToClient.GameResult:

                        DefinedStructure.Packet_GameResult pGameResult = new DefinedStructure.Packet_GameResult();
                        pGameResult = (DefinedStructure.Packet_GameResult)ConvertPacket.ByteArrayToStructure(pToClient._data, pGameResult.GetType(), pToClient._totalSize);

                        IngameManager._instance.ShowGameResult(_myName.Equals(pGameResult._name));

                        break;
                    #endregion
                }
            }

            yield return null;
        }
    }

    IEnumerator SendOrder()
    {
        while(true)
        {
            if(_fromClientQueue.Count != 0)
                _server.Send(_fromClientQueue.Dequeue());

            yield return null;
        }
    }

    public void ConnectServer()
    {   
        _isConnect = Connect(_ip, _port);
    }

    public void SelectComplete(int[] idx)
    {
        DefinedStructure.Packet_ChooseCard pChooseCard;
        pChooseCard._UUID = _myUUID;
        pChooseCard._roomNumber = _myRoomNum;
        pChooseCard._cardIdx1 = idx[0];
        pChooseCard._cardIdx2 = idx[1];
        pChooseCard._slotIndex = IngameManager._instance._MyIndex;

        ToPacket(DefinedProtocol.eFromClient.ChooseCard, pChooseCard);
    }

    public void CreateRoom(string roomName, bool isLock, string pw)
    {
        DefinedStructure.Packet_CreateRoom pCreateRoom;
        pCreateRoom._roomName = roomName;
        if (isLock)
        {
            pCreateRoom._isLock = 0;
            pCreateRoom._pw = pw;
        }   
        else
        {
            pCreateRoom._isLock = 1;
            pCreateRoom._pw = "NO_PW";
        }

        ToPacket(DefinedProtocol.eFromClient.CreateRoom, pCreateRoom);
    }

    public void EnterRoom(int roomNum, string pw)
    {
        _myRoomNum = roomNum;

        DefinedStructure.Packet_EnterRoom pEnterRoom;
        pEnterRoom._roomNumber = roomNum;
        pEnterRoom._pw = pw;

        ToPacket(DefinedProtocol.eFromClient.EnterRoom, pEnterRoom);
    }

    public void OverlapCheck_ID(string id)
    {
        DefinedStructure.Packet_OverlapCheckID pOverlapCheck_ID;
        pOverlapCheck_ID._id = id;
        pOverlapCheck_ID._index = -999;

        ToPacket(DefinedProtocol.eFromClient.OverlapCheck_ID, pOverlapCheck_ID);
    }

    public void JoinGame(string id, string pw)
    {
        DefinedStructure.Packet_JoinGame pJoinGame;
        pJoinGame._id = id;
        pJoinGame._pw = pw;
        pJoinGame._index = -999;

        ToPacket(DefinedProtocol.eFromClient.JoinGame, pJoinGame);
    }

    public void LogIn(string id, string pw)
    {
        DefinedStructure.Packet_LogIn pLogIn;
        pLogIn._id = id;
        pLogIn._pw = pw;
        pLogIn._index = -999;

        ToPacket(DefinedProtocol.eFromClient.LogIn, pLogIn);
    }

    public void MyInfo(string nickName, int avatar)
    {
        _myName = nickName;
        _myAvatar = avatar;

        DefinedStructure.Packet_MyInfo pMyInfo;
        pMyInfo._UUID = _myUUID;
        pMyInfo._name = nickName;
        pMyInfo._avatarIndex = avatar;
        pMyInfo._slotIndex = -999;

        ToPacket(DefinedProtocol.eFromClient.MyInfo, pMyInfo);
    }

    public void AddAI(int index)
    {
        DefinedStructure.Packet_AddAI pAddAI;
        pAddAI._roomNumber = _myRoomNum;
        pAddAI._index = index;

        ToPacket(DefinedProtocol.eFromClient.AddAI, pAddAI);
    }

    public void ReadyForGame()
    {
        DefinedStructure.Packet_Ready pReady;
        pReady._roomNumber = _myRoomNum;
        pReady._slotIndex = IngameManager._instance._MyIndex;

        ToPacket(DefinedProtocol.eFromClient.Ready, pReady);
    }

    public void GameStart()
    {
        DefinedStructure.Packet_GameStart pGameStart;
        pGameStart._roomNumber = _myRoomNum;

        ToPacket(DefinedProtocol.eFromClient.GameStart, pGameStart);
    }

    public void ExitRoom()
    {
        DefinedStructure.Packet_ExitRoom pExitRoom;
        pExitRoom._roomNumber = _myRoomNum;
        pExitRoom._UUID = _myUUID;
        pExitRoom._slotIndex = IngameManager._instance._MyIndex;

        ToPacket(DefinedProtocol.eFromClient.ExitRoom, pExitRoom);
    }
}
