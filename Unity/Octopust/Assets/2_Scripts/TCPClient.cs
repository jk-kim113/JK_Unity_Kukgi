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
        if(_server != null && _server.Poll(0, SelectMode.SelectRead))
        {
            byte[] buffer = new byte[1024];
            int recvLen = _server.Receive(buffer);
            if(recvLen > 0)
            {
                string log = System.Text.Encoding.UTF8.GetString(buffer);
                _chatWnd.AddLogChat(log);
            }
        }
    }

    bool Connect(string ipAddress, int port)
    {
        try
        {
            // make socket
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _server.Connect(ipAddress, port);
            _chatWnd.AddLogChat("서버에 접속 하였습니다.");
            return true;
        }
        catch(System.Exception ex)
        {
            // 메세지 창에 띄운다.
            _chatWnd.AddLogChat(ex.Message);
        }

        return false;
    }

    public void ClickConnectButton()
    {
        Connect(_ip, _port);
    }

    public void SendMsg(string msg)
    {
        if(!msg.Equals(string.Empty))
        {
            // 패킷을 만들어서 보낸다.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg);
            _server.Send(buffer);
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
            string a = "클라이언트 접속 종료";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(a);
            _server.Send(buffer);
            _server.Shutdown(SocketShutdown.Both);
            _server.Close();
            _server = null;
        }
    }
}
