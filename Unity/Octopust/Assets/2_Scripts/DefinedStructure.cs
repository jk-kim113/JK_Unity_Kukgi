using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class DefinedStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PacketInfo                                        // 전체 사이즈 1032byte
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _id;
        [MarshalAs(UnmanagedType.I4)]
        public int _totalSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] _data;
    }

    public struct Packet_Login
    {
        public long _UUID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_LoginAfter
    {
        [MarshalAs(UnmanagedType.I8)]
        public long _UUID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string _name;
        [MarshalAs(UnmanagedType.I4)]
        public int _avatarIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_Chat
    {
        [MarshalAs(UnmanagedType.I8)]
        public long _UUID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string _chat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_CurrentUser
    {
        [MarshalAs(UnmanagedType.I8)]
        public long _UUID;
        [MarshalAs(UnmanagedType.I4)]
        public int _avatarIndex;
    }

    #region Previous Strut
    //[StructLayout(LayoutKind.Sequential)]
    //public struct SendChatMessage
    //{
    //    [MarshalAs(UnmanagedType.I4)]
    //    public DefinedProtocol.eSendMessage _id;
    //    [MarshalAs(UnmanagedType.I8)]
    //    public long _UUID;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)] //10글자
    //    public string _name;
    //    [MarshalAs(UnmanagedType.I4)]
    //    public int _avatarIndex;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)] //50글자
    //    public string _chatting;
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //public struct ReceiveChatMessage
    //{
    //    [MarshalAs(UnmanagedType.I4)]
    //    public DefinedProtocol.eReceiveMessage _id;
    //    public long _UUID;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)] //10글자
    //    public string _name;
    //    [MarshalAs(UnmanagedType.I4)]
    //    public int _avatarIndex;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)] //50글자
    //    public string _chatting;
    //}
    #endregion
}
