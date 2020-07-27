using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _chatText;
#pragma warning restore

    public void SetChatting(string text)
    {
        _chatText.text = text;
    }
}
