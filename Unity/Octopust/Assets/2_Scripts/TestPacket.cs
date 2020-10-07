using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPacket : MonoBehaviour
{
    byte[] _buffer = new byte[1024];

    //private void Start()
    //{
    //    CheckSend();
    //    CheckReceive();
    //}

    //void CheckSend()
    //{
    //    DefinedStructure.SendChatMessage send;
    //    send._id = DefinedProtocol.eSendMessage.Chatting_Message;
    //    send._UUID = 12345;
    //    send._name = "qqq";
    //    send._avatarIndex = 1;
    //    send._chatting = "유저가 입장하였습니다.";

    //    _buffer = ConvertPacket.StructureToByteArray(send);
    //}

    //void CheckReceive()
    //{
    //    //DefinedStructure.SendChatMessage send = new DefinedStructure.SendChatMessage();
    //    //send = (DefinedStructure.SendChatMessage)ConvertPacket.ByteArrayToStructure(_buffer, send.GetType());

    //    DefinedStructure.SendChatMessage send = new DefinedStructure.SendChatMessage();
    //    send = (DefinedStructure.SendChatMessage)ConvertPacket.ByteArrayToStructure(_buffer, send);

    //    Debug.Log(send._id);
    //    Debug.Log(send._UUID);
    //    Debug.Log(send._name);
    //    Debug.Log(send._avatarIndex);
    //    Debug.Log(send._chatting);
    //}
}
