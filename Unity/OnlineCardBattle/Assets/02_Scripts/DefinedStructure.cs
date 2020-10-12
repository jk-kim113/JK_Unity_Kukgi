﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

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

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_Connect
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string _name;
        [MarshalAs(UnmanagedType.I4)]
        public int _avatarIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_CheckConnect
    {
        [MarshalAs(UnmanagedType.I8)]
        public long _UUID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_ShowRoomInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _roomNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string _roomName;
        [MarshalAs(UnmanagedType.I4)]
        public int _isLock;
        [MarshalAs(UnmanagedType.I4)]
        public int _currentMemberNum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_ChooseCard
    {
        [MarshalAs(UnmanagedType.I8)]
        public long _UUID;
        [MarshalAs(UnmanagedType.I4)]
        public int _roomNumber;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx1;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_NextTurn
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string _name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_ChooseInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx1;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx2;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardImgIdx1;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardImgIdx2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_ChooseResult
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string _name;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx1;
        [MarshalAs(UnmanagedType.I4)]
        public int _cardIdx2;
        [MarshalAs(UnmanagedType.I4)]
        public int _isSuccess;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_GameResult
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string _name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_CreateRoom
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string _roomName;
        [MarshalAs(UnmanagedType.I4)]
        public int _isLock;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string _pw;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_AfterCreateRoom
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _roomNumber;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Packet_EnterRoom
    {
        [MarshalAs(UnmanagedType.I4)]
        public int _roomNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string _pw;
    }
}
