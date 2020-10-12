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

                DefinedStructure.Packet_Connect pConnect;
                pConnect._name = _myName;
                pConnect._avatarIndex = _myAvatar;

                ToPacket(DefinedProtocol.eFromClient.Connect, pConnect);

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
                    case DefinedProtocol.eToClient.CheckConnect:

                        DefinedStructure.Packet_CheckConnect pCheckConnect = new DefinedStructure.Packet_CheckConnect();
                        pCheckConnect = (DefinedStructure.Packet_CheckConnect)ConvertPacket.ByteArrayToStructure(pToClient._data, pCheckConnect.GetType(), pToClient._totalSize);

                        _myUUID = pCheckConnect._UUID;

                        LogInManager._instace.SceneChange();

                        break;
                    #endregion

                    #region 방 입장 관련 부분
                    case DefinedProtocol.eToClient.SuccessEnterRoom:

                        SceneManager.LoadScene("IngameScene");
                        
                        break;

                    case DefinedProtocol.eToClient.FailEnterRoom:

                        LobbyManager._instance.FailEnterRoom();

                        break;

                    case DefinedProtocol.eToClient.AfterCreateRoom:

                        DefinedStructure.Packet_AfterCreateRoom pAfterCreateRoom = new DefinedStructure.Packet_AfterCreateRoom();
                        pAfterCreateRoom = (DefinedStructure.Packet_AfterCreateRoom)ConvertPacket.ByteArrayToStructure(pToClient._data, pAfterCreateRoom.GetType(), pToClient._totalSize);

                        _myRoomNum = pAfterCreateRoom._roomNumber;

                        break;

                    case DefinedProtocol.eToClient.ShowRoomInfo:

                        DefinedStructure.Packet_ShowRoomInfo pShowRoomInfo = new DefinedStructure.Packet_ShowRoomInfo();
                        pShowRoomInfo = (DefinedStructure.Packet_ShowRoomInfo)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowRoomInfo.GetType(), pToClient._totalSize);

                        LobbyManager._instance.ShowRoomInfo(pShowRoomInfo._roomNumber, pShowRoomInfo._roomName, pShowRoomInfo._isLock == 0 ? true : false, pShowRoomInfo._currentMemberNum);

                        break;
                    #endregion

                    #region 접속시 타 유저에게 내정보를 알려주고 타 유저의 정보를 받아오는 곳
                    case DefinedProtocol.eToClient.ShowUserInfo:

                        while(!_isIngame)
                        {
                            yield return null;
                        }

                        DefinedStructure.Packet_Connect pShowUser = new DefinedStructure.Packet_Connect();
                        pShowUser = (DefinedStructure.Packet_Connect)ConvertPacket.ByteArrayToStructure(pToClient._data, pShowUser.GetType(), pToClient._totalSize);

                        if (!pShowUser._name.Equals(_myName))
                        {
                            IngameManager._instance.ShowOtherUser(pShowUser._name, pShowUser._avatarIndex);
                        }

                        break;
                    #endregion

                    #region 게임 시작을 알려주는 곳
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

                        IngameManager._instance.ReverseCard(pChooseInfo._cardIdx1, pChooseInfo._cardIdx2, pChooseInfo._cardImgIdx1, pChooseInfo._cardImgIdx2);

                        break;

                    case DefinedProtocol.eToClient.ChooseResult:

                        DefinedStructure.Packet_ChooseResult pChooseResult = new DefinedStructure.Packet_ChooseResult();
                        pChooseResult = (DefinedStructure.Packet_ChooseResult)ConvertPacket.ByteArrayToStructure(pToClient._data, pChooseResult.GetType(), pToClient._totalSize);

                        if(pChooseResult._isSuccess == 0)
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
            {
                _server.Send(_fromClientQueue.Dequeue());
            }

            yield return null;
        }
    }

    public void ConnectServer(string name, int avaIdx)
    {
        _myName = name;
        _myAvatar = avaIdx;

        _isConnect = Connect(_ip, _port);
    }

    public void SelectComplete(int[] idx)
    {
        DefinedStructure.Packet_ChooseCard pChooseCard;
        pChooseCard._UUID = _myUUID;
        pChooseCard._roomNumber = _myRoomNum;
        pChooseCard._cardIdx1 = idx[0];
        pChooseCard._cardIdx2 = idx[1];

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
}
