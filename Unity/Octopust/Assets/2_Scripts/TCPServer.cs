using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public struct rPacketData
{
    public byte[] _buffer;
    public int _bufferLength;
    public int _index;

    public rPacketData(byte[] buffer, int size, int idx)
    {
        _buffer = buffer;
        _bufferLength = size;
        _index = idx;
    }
}

public struct sPacketData
{
    public byte[] _buffer;
    public int _index;
    
    public sPacketData(byte[] buffer, int idx)
    {
        _buffer = buffer;
        _index = idx;
    }
}

public class TCPServer : MonoBehaviour
{
    #region 관련 변수
    static TCPServer _uniqueInstance;
    public static TCPServer _instance { get { return _uniqueInstance; } }

    const short _port = 80;
    Socket _waitServer;
    //Socket _client;
    List<Socket> _clients = new List<Socket>();

    ServerLogUI _logUI;

    Queue<rPacketData> _receiveQueue = new Queue<rPacketData>();
    Queue<sPacketData> _sendQueue = new Queue<sPacketData>();

    long _nowUUID = 1000000001;

    Dictionary<long, int> _currentUsers = new Dictionary<long, int>();
    Dictionary<long, string> _currentUserName = new Dictionary<long, string>();

    List<string> _totalLog = new List<string>();
    #endregion

    private void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {   
        LoadLogData();
        _logUI = GameObject.FindGameObjectWithTag("LogUIWindow").GetComponent<ServerLogUI>();

        CreateServer();

        StartCoroutine(AddOrder());
        StartCoroutine(DoOrder());
        StartCoroutine(SendOrder());

    }

    IEnumerator SendOrder()
    {
        while(true)
        {
            if(_sendQueue.Count != 0)
            {
                sPacketData sPacket = _sendQueue.Dequeue();

                if (sPacket._index < 0)
                {
                    SendToClient(sPacket._buffer);
                }
                else
                {
                    _clients[sPacket._index].Send(sPacket._buffer);
                }
            }

            yield return null;
        }
    }

    IEnumerator AddOrder()
    {
        while(true)
        {
            if (_clients.Count != 0)
            {
                for (int n = 0; n < _clients.Count; n++)
                {
                    if (_clients[n].Poll(0, SelectMode.SelectRead))
                    {
                        byte[] sendBuffer = new byte[1032];

                        try
                        {
                            int recvLen = _clients[n].Receive(sendBuffer);
                            if (recvLen > 0)
                            {
                                _receiveQueue.Enqueue(new rPacketData(sendBuffer, recvLen, n));
                            }
                        }
                        catch (Exception ex)
                        {
                            _logUI.AddLogChat(ex.Message);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator DoOrder()
    {
        while (true)
        {
            if (_receiveQueue.Count != 0)
            {
                rPacketData rPacket = _receiveQueue.Dequeue();

                try
                {
                    DefinedStructure.PacketInfo packetSend = new DefinedStructure.PacketInfo();
                    packetSend = (DefinedStructure.PacketInfo)ConvertPacket.ByteArrayToStructure(rPacket._buffer, packetSend.GetType(), rPacket._bufferLength);

                    string logMessage = string.Empty;
                    switch ((DefinedProtocol.eSendMessage)packetSend._id)
                    {
                        case DefinedProtocol.eSendMessage.Connect_Message:

                            DefinedStructure.Packet_Login packet_login;
                            packet_login._UUID = _nowUUID;
                            _nowUUID++;

                            DefinedStructure.PacketInfo packetRecieve1;
                            packetRecieve1._id = (int)DefinedProtocol.eReceiveMessage.Connect_User;
                            packetRecieve1._data = ConvertPacket.StructureToByteArray(packet_login);
                            packetRecieve1._totalSize = packetRecieve1._data.Length;
                            _sendQueue.Enqueue(new sPacketData(ConvertPacket.StructureToByteArray(packetRecieve1), rPacket._index));

                            if(_currentUsers.Count != 0)
                            {
                                foreach(long key in _currentUsers.Keys)
                                {
                                    DefinedStructure.Packet_CurrentUser packetCurrent;
                                    packetCurrent._UUID = key;
                                    packetCurrent._avatarIndex = _currentUsers[key];

                                    DefinedStructure.PacketInfo packetRecieve5;
                                    packetRecieve5._id = (int)DefinedProtocol.eReceiveMessage.Current_User;
                                    packetRecieve5._data = ConvertPacket.StructureToByteArray(packetCurrent);
                                    packetRecieve5._totalSize = packetRecieve5._data.Length;
                                    _sendQueue.Enqueue(new sPacketData(ConvertPacket.StructureToByteArray(packetRecieve5), rPacket._index));
                                }
                            }

                            break;

                        case DefinedProtocol.eSendMessage.Connect_After:

                            DefinedStructure.Packet_LoginAfter packet_loginAfter = new DefinedStructure.Packet_LoginAfter();
                            packet_loginAfter = (DefinedStructure.Packet_LoginAfter)ConvertPacket.ByteArrayToStructure(packetSend._data, packet_loginAfter.GetType(), packetSend._totalSize);

                            _currentUsers.Add(packet_loginAfter._UUID, packet_loginAfter._avatarIndex);
                            _currentUserName.Add(packet_loginAfter._UUID, packet_loginAfter._name);

                            logMessage = string.Format("{0} 유저 접속\t\t{1}", packet_loginAfter._name, DateTime.Now);

                            DefinedStructure.PacketInfo packetRecieve2;
                            packetRecieve2._id = (int)DefinedProtocol.eReceiveMessage.Connect_After;
                            packetRecieve2._data = ConvertPacket.StructureToByteArray(packet_loginAfter);
                            packetRecieve2._totalSize = packetRecieve2._data.Length;
                            _sendQueue.Enqueue(new sPacketData(ConvertPacket.StructureToByteArray(packetRecieve2), -999));

                            break;

                        case DefinedProtocol.eSendMessage.Chatting_Message:

                            DefinedStructure.Packet_Chat packet_chat = new DefinedStructure.Packet_Chat();
                            packet_chat = (DefinedStructure.Packet_Chat)ConvertPacket.ByteArrayToStructure(packetSend._data, packet_chat.GetType(), packetSend._totalSize);
                            packet_chat._chat = string.Format("{0} : {1}", _currentUserName[packet_chat._UUID], packet_chat._chat);

                            DefinedStructure.PacketInfo packetRecieve3;
                            packetRecieve3._id = (int)DefinedProtocol.eReceiveMessage.Chatting_Message;
                            packetRecieve3._data = ConvertPacket.StructureToByteArray(packet_chat);
                            packetRecieve3._totalSize = packetRecieve3._data.Length;
                            _sendQueue.Enqueue(new sPacketData(ConvertPacket.StructureToByteArray(packetRecieve3), -999));

                            logMessage = string.Format("{0}\t\t{1}", packet_chat._chat, DateTime.Now);

                            break;

                        case DefinedProtocol.eSendMessage.Disconnect_Message:

                            DefinedStructure.Packet_Login packet_logOut = new DefinedStructure.Packet_Login();
                            packet_logOut = (DefinedStructure.Packet_Login)ConvertPacket.ByteArrayToStructure(packetSend._data, packet_logOut.GetType(), packetSend._totalSize);

                            DefinedStructure.Packet_LoginAfter packet_out;
                            packet_out._UUID = packet_logOut._UUID;
                            packet_out._name = _currentUserName[packet_logOut._UUID];
                            packet_out._avatarIndex = _currentUsers[packet_logOut._UUID];

                            logMessage = string.Format("{0} 유저 퇴장\t\t{1}", packet_out._name, DateTime.Now);

                            DefinedStructure.PacketInfo packetRecieve4;
                            packetRecieve4._id = (int)DefinedProtocol.eReceiveMessage.Disconnect_User;
                            packetRecieve4._data = ConvertPacket.StructureToByteArray(packet_out);
                            packetRecieve4._totalSize = packetRecieve4._data.Length;
                            _sendQueue.Enqueue(new sPacketData(ConvertPacket.StructureToByteArray(packetRecieve4), -999));

                            _currentUserName.Remove(packet_logOut._UUID);
                            _currentUsers.Remove(packet_logOut._UUID);

                            _clients[rPacket._index].Shutdown(SocketShutdown.Both);
                            _clients[rPacket._index].Close();
                            _clients[rPacket._index] = null;
                            _clients.RemoveAt(rPacket._index);

                            break;
                    }

                    if(!string.IsNullOrEmpty(logMessage))
                    {
                        _totalLog.Add(logMessage);
                        _logUI.AddLogChat(logMessage);
                    }
                }
                catch (Exception ex)
                {
                    _logUI.AddLogChat(ex.Message);
                }
            }
            yield return null;
        }
    }
    
    void SendToClient(byte[] buffer)
    {
        for (int n = 0; n < _clients.Count; n++)
        {
            if (_clients[n] != null)
                _clients[n].Send(buffer);
        }
    }

    private void Update()
    {
        // 소켓에 들어온 패킷을 읽는다.
        if (_waitServer != null && _waitServer.Poll(0, SelectMode.SelectRead))
        {
            //_client = _waitServer.Accept();
            Socket add = _waitServer.Accept();
            _clients.Add(add);
        }
    }

    void CreateServer()
    {
        try
        {
            // make socket
            _waitServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 주소와 포트를 Bind한다.
            _waitServer.Bind(new IPEndPoint(IPAddress.Any, _port));
            // 서버를 대기상태로 바꾼다.
            _waitServer.Listen(1);

            _logUI.AddLogChat("소켓이 만들어 졌습니다.");
        }
        catch(Exception ex)
        {
            // log window에 표시
            _logUI.AddLogChat(ex.Message);
        }
    }

    public void MyChatting(string chat)
    {
        // 상대에게 메세지 보내고 내꺼에 출력하고.
        if(!chat.Equals(string.Empty))
        {   
            if(_clients.Count != 0)
            {
                byte[] msg = System.Text.Encoding.UTF8.GetBytes(chat);
                for(int n = 0; n < _clients.Count; n++)
                {
                    _clients[n].Send(msg);
                }
            }
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
        SaveLogData();

        if (_clients.Count != 0)
        {
            for (int n = 0; n < _clients.Count; n++)
            {   
                _clients[n].Shutdown(SocketShutdown.Both);
                _clients[n].Close();
                _clients[n] = null;
            }

            _clients.Clear();
        }

        if(_waitServer != null)
        {
            _waitServer.Close();
            _waitServer = null;
        }
    }

    void SaveLogData()
    {
        string destination = Application.dataPath + "/bin/totalLog.dat";
        FileStream file;

        if (File.Exists(destination)) 
            file = File.OpenWrite(destination);
        else 
            file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, _totalLog);
        file.Close();
    }

    void LoadLogData()
    {
        string destination = Application.dataPath + "/bin/totalLog.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogWarning("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<string> logData = (List<string>)bf.Deserialize(file);
        file.Close();

        for(int n = 0; n < logData.Count; n++)
        {
            _totalLog.Add(logData[n]);
            Debug.Log(logData[n]);
        }
    }
}
