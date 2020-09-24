using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLogUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    ScrollRect _scrollRect;
    [SerializeField]
    Scrollbar _scrollBar;
    [SerializeField]
    InputField _inputChat;
#pragma warning restore

    GameObject _prefabText;

    private void Awake()
    {
        _prefabText = Resources.Load("Text") as GameObject;
    }

    private void Update()
    {
        if(!string.IsNullOrEmpty(_inputChat.text))
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                ClickSendButton();
            }
            else
            {
                if(!_inputChat.isFocused && Input.GetKeyDown(KeyCode.Return))
                {
                    _inputChat.ActivateInputField();
                }
            }
        }
    }

    public void AddLogChat(string msg)
    {
        GameObject go = Instantiate(_prefabText, _scrollRect.content);
        Text add = go.GetComponent<Text>();
        add.text = msg;
    }

    public void ClickSendButton()
    {
        TCPServer._instance.MyChatting(_inputChat.text);
        _inputChat.text = string.Empty;
    }
}
