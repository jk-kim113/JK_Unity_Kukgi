using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class TCPServer : MonoBehaviour
{
    static TCPServer _uniqueInstance;
    public static TCPServer _instance { get { return _uniqueInstance; } }

    const short _port = 80;
    Socket _waitServer;
    Socket _client;

    ServerLogUI _logUI;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        _logUI = GameObject.FindGameObjectWithTag("LogUIWindow").GetComponent<ServerLogUI>();

        CreateServer();
    }

    private void Update()
    {
        // 소켓에 들어온 패킷을 읽는다.
        if(_waitServer.Poll(0, SelectMode.SelectRead))
        {
            _client = _waitServer.Accept();
            _logUI.AddLogChat("유저가 접속했습니다.");
        }

        if (_client != null && _client.Poll(0, SelectMode.SelectRead))
        {
            byte[] buffer = new byte[1024];
            try
            {
                int recvLen = _client.Receive(buffer);
                if (recvLen > 0)
                {
                    _client.Send(buffer);
                    string log = System.Text.Encoding.UTF8.GetString(buffer);
                    _logUI.AddLogChat(log);
                }
            }
            catch(Exception ex)
            {
                _logUI.AddLogChat(ex.Message);
            }
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
            if(_client != null)
            {
                // 패킷을 만든다.
                byte[] msg = System.Text.Encoding.UTF8.GetBytes(chat);
                _client.Send(msg);
            }

            // 내 로그 창에 메세지를 띄운다.
            _logUI.AddLogChat(chat);
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
        if(_client != null)
        {
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
            _client = null;
        }

        if(_waitServer != null)
        {
            _waitServer.Close();
            _waitServer = null;
        }
    }
}
