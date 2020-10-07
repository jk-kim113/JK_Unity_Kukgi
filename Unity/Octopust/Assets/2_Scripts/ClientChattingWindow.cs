using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientChattingWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    ScrollRect _scrollRect;
    [SerializeField]
    InputField _inputChat;
    [SerializeField]
    Image _myImg;
    [SerializeField]
    Image[] _otherImg;
#pragma warning restore

    GameObject _prefabText;

    long[] _otherUUID;

    private void Awake()
    {
        _prefabText = Resources.Load("Text") as GameObject;
    }

    private void Start()
    {
        for (int n = 0; n < _otherImg.Length; n++)
        {
            _otherImg[n].sprite = null;
        }

        _otherUUID = new long[_otherImg.Length];
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(_inputChat.text))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ClickSendButton();
            }
            else
            {
                if (!_inputChat.isFocused && Input.GetKeyDown(KeyCode.Return))
                {
                    _inputChat.ActivateInputField();
                }
            }
        }
    }

    public void AddLogChat(string msg, bool isMine)
    {
        GameObject go = Instantiate(_prefabText, _scrollRect.content);
        Text add = go.GetComponent<Text>();

        if (isMine)
        {
            add.color = Color.white;
        }
        else
        {
            add.color = Color.yellow;
        }

        add.text = msg;
    }

    public void ClickSendButton()
    {
        TCPClient._instance.SendMsg(DefinedProtocol.eSendMessage.Chatting_Message, _inputChat.text);
        _inputChat.text = string.Empty;
    }

    public void ShowMyImg(Sprite img)
    {
        _myImg.sprite = img;
    }

    public void ShowOtherImg(Sprite img, long UUID)
    {
        for(int n = 0; n < _otherUUID.Length; n++)
        {
            if(_otherUUID[n] == UUID)
            {
                return;
            }
        }

        for(int n = 0; n < _otherImg.Length;n++)
        {
            if(_otherImg[n].sprite == null)
            {
                _otherImg[n].sprite = img;
                _otherUUID[n] = UUID;
                break;
            }   
        }
    }

    public void OffOtherImg(long UUID)
    {
        for (int n = 0; n < _otherUUID.Length; n++)
        {
            if (_otherUUID[n] == UUID)
            {
                _otherUUID[n] = 0;
                _otherImg[n].sprite = null;
                return;
            }
        }
    }
}
