using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class TCPClient : MonoBehaviour
{
    static TCPClient _uniqueInstance;
    public static TCPClient _instance { get { return _uniqueInstance; } }

    const string _ip = "127.0.0.1";
    const int _port = 80;

    Socket _server;
    string _sendMsg = string.Empty;
    string _recvMsg = string.Empty;

    List<string> _chatMessages = new List<string>();

    ClientChattingWindow _chatWnd;

    long _myUUID = 0;
    bool _isConnect = false;
    bool _isDisConnect = false;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _chatWnd = GameObject.Find("ClientUIMain").GetComponent<ClientChattingWindow>();
    }

    private void Update()
    {
        if(_isConnect && _server != null && _server.Poll(0, SelectMode.SelectRead))
        {
            byte[] buffer = new byte[1032];
            int recvLen = _server.Receive(buffer);
            if(recvLen > 0)
            {
                DefinedStructure.PacketInfo packetReceive = new DefinedStructure.PacketInfo();
                packetReceive = (DefinedStructure.PacketInfo)ConvertPacket.ByteArrayToStructure(buffer, packetReceive.GetType(), recvLen);

                switch((DefinedProtocol.eReceiveMessage)packetReceive._id)
                {
                    case DefinedProtocol.eReceiveMessage.Connect_User:

                        DefinedStructure.Packet_Login packet_login = new DefinedStructure.Packet_Login();
                        packet_login = (DefinedStructure.Packet_Login)ConvertPacket.ByteArrayToStructure(packetReceive._data, packet_login.GetType(), packetReceive._totalSize);

                        _myUUID = packet_login._UUID;
                        
                        DefinedStructure.Packet_LoginAfter packet_loginAfter;
                        packet_loginAfter._UUID = _myUUID;
                        packet_loginAfter._name = ClientManager._instance._nowName;
                        packet_loginAfter._avatarIndex = ClientManager._instance._characID;

                        DefinedStructure.PacketInfo packetSend;
                        packetSend._id = (int)DefinedProtocol.eSendMessage.Connect_After;
                        packetSend._data = ConvertPacket.StructureToByteArray(packet_loginAfter);
                        packetSend._totalSize = packetSend._data.Length;
                        _server.Send(ConvertPacket.StructureToByteArray(packetSend));

                        break;

                    case DefinedProtocol.eReceiveMessage.Connect_After:

                        DefinedStructure.Packet_LoginAfter packet_loginAfterReceive = new DefinedStructure.Packet_LoginAfter();
                        packet_loginAfterReceive = (DefinedStructure.Packet_LoginAfter)ConvertPacket.ByteArrayToStructure(packetReceive._data, packet_loginAfterReceive.GetType(), packetReceive._totalSize);

                        if(packet_loginAfterReceive._UUID != _myUUID)
                        {
                            string log = string.Format("{0} 유저가 입장 하였습니다.", packet_loginAfterReceive._name);
                            _chatWnd.AddLogChat(log, false);
                            _chatWnd.ShowOtherImg(ClientManager._instance._selectedImg[packet_loginAfterReceive._avatarIndex], packet_loginAfterReceive._UUID);
                        }

                        break;

                    case DefinedProtocol.eReceiveMessage.Current_User:

                        DefinedStructure.Packet_CurrentUser packet_User = new DefinedStructure.Packet_CurrentUser();
                        packet_User = (DefinedStructure.Packet_CurrentUser)ConvertPacket.ByteArrayToStructure(packetReceive._data, packet_User.GetType(), packetReceive._totalSize);

                        _chatWnd.ShowOtherImg(ClientManager._instance._selectedImg[packet_User._avatarIndex], packet_User._UUID);

                        break;

                    case DefinedProtocol.eReceiveMessage.Chatting_Message:

                        DefinedStructure.Packet_Chat packet_chat = new DefinedStructure.Packet_Chat();
                        packet_chat = (DefinedStructure.Packet_Chat)ConvertPacket.ByteArrayToStructure(packetReceive._data, packet_chat.GetType(), packetReceive._totalSize);

                        _chatWnd.AddLogChat(packet_chat._chat, _myUUID == packet_chat._UUID);

                        break;

                    case DefinedProtocol.eReceiveMessage.Disconnect_User:

                        DefinedStructure.Packet_LoginAfter packet_out = new DefinedStructure.Packet_LoginAfter();
                        packet_out = (DefinedStructure.Packet_LoginAfter)ConvertPacket.ByteArrayToStructure(packetReceive._data, packet_out.GetType(), packetReceive._totalSize);

                        if(packet_out._UUID == _myUUID)
                        {
                            _isDisConnect = true;
                            _isConnect = false;
                        }
                        else
                        {
                            string outLog = string.Format("{0} 유저가 접속을 종료했습니다.", packet_out._name);
                            _chatWnd.AddLogChat(outLog, false);
                            _chatWnd.OffOtherImg(packet_out._UUID);
                        }

                        break;
                }
            }
        }
    }

    public void ChatStart()
    {
        _chatWnd.ShowMyImg(ClientManager._instance._selectedImg[ClientManager._instance._characID]);
    }

    bool Connect(string ipAddress, int port)
    {
        try
        {
            if(!_isConnect)
            {
                // make socket
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _server.Connect(ipAddress, port);

                // 로그인 시도
                DefinedStructure.Packet_LoginAfter packet_loginAfter;
                packet_loginAfter._UUID = 0;
                packet_loginAfter._name = ClientManager._instance._nowName;
                packet_loginAfter._avatarIndex = ClientManager._instance._characID;

                DefinedStructure.PacketInfo packetInfo;
                packetInfo._id = (int)DefinedProtocol.eSendMessage.Connect_Message;
                packetInfo._data = ConvertPacket.StructureToByteArray(packet_loginAfter);
                packetInfo._totalSize = packetInfo._data.Length;

                byte[] packetBuffer = new byte[1032];
                packetBuffer = ConvertPacket.StructureToByteArray(packetInfo);
                _server.Send(packetBuffer);

                _chatWnd.AddLogChat("서버에 접속 하였습니다.", true);
                return true;
            }
        }
        catch(System.Exception ex)
        {
            // 메세지 창에 띄운다.
            _chatWnd.AddLogChat(ex.Message, true);
        }

        return false;
    }

    public void ClickConnectButton()
    {
        _isConnect = Connect(_ip, _port);
    }

    public void SendMsg(DefinedProtocol.eSendMessage msgType, string msg)
    {
        if (!msg.Equals(string.Empty))
        {
            // 패킷을 만들어서 보낸다.
            DefinedStructure.Packet_Chat packet_chat;
            packet_chat._UUID = _myUUID;
            packet_chat._chat = msg;

            DefinedStructure.PacketInfo packetInfo;
            packetInfo._id = (int)DefinedProtocol.eSendMessage.Chatting_Message;
            packetInfo._data = ConvertPacket.StructureToByteArray(packet_chat);
            packetInfo._totalSize = packetInfo._data.Length;
            
            _server.Send(ConvertPacket.StructureToByteArray(packetInfo));
        }
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        OnApplicationQuit();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnApplicationQuit()
    {
        if (_server != null)
        {
            DefinedStructure.Packet_Login packet_LogOut;
            packet_LogOut._UUID = _myUUID;

            DefinedStructure.PacketInfo packetInfo;
            packetInfo._id = (int)DefinedProtocol.eSendMessage.Disconnect_Message;
            packetInfo._data = ConvertPacket.StructureToByteArray(packet_LogOut);
            packetInfo._totalSize = packetInfo._data.Length;
            _server.Send(ConvertPacket.StructureToByteArray(packetInfo));

            StartCoroutine(QuitSafely());
        }
    }

    IEnumerator QuitSafely()
    {
        while(!_isDisConnect)
        {
            yield return null;
        }

        _server.Shutdown(SocketShutdown.Both);
        _server.Close();
        _server = null;
    }
}
