using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ConvertPacket
{
    //public static byte[] StructureToByteArray(object obj)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    MemoryStream stream = new MemoryStream();

    //    formatter.Serialize(stream, obj);
    //    byte[] buffer = stream.GetBuffer();
    //    stream.Close();

    //    return buffer;
    //}

    //public static object ByteArrayToStructure(byte[] data)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    MemoryStream stream = new MemoryStream(data);

    //    object obj = formatter.Deserialize(stream);

    //    stream.Close();

    //    return obj;
    //}

    public static byte[] StructureToByteArray(object obj)
    {
        // 구조체에 할당된 메모리의 크기를 구한다.
        int datasize = Marshal.SizeOf(obj);
        // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.
        IntPtr buff = Marshal.AllocHGlobal(datasize);
        // 할당된 구조체 객체의 주소를 구한다.
        Marshal.StructureToPtr(obj, buff, false);
        // 구조체가 보가될 배열
        byte[] data = new byte[datasize];
        // 구조체 객체를 배열에 복사한다.
        Marshal.Copy(buff, data, 0, datasize);
        Marshal.FreeHGlobal(buff);
        // 비관리 메모리 영역에 할당했던 메모리를 해제한다.

        // 배열을 리턴
        return data;
    }

    public static object ByteArrayToStructure(byte[] data, Type type, int size)
    {
        // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
        IntPtr buff = Marshal.AllocHGlobal(data.Length);
        // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
        Marshal.Copy(data, 0, buff, data.Length);
        // 복사된 데이터를 구조체 객체로 변환한다.
        object obj = Marshal.PtrToStructure(buff, type);
        // 비관리 메모리 영역에 할당했던 메모리를 해제한다.
        Marshal.FreeHGlobal(buff);

        // 구조체와 원래의 데이터의 크기 비교.
        if (Marshal.SizeOf(obj) != size)
            return null; // 크기가 다르면 null 반환.

        // 구조체 리턴
        return obj;
    }

    //public static object ByteArrayToStructure(byte[] data, object myStruct)
    //{
    //    int size = Marshal.SizeOf(myStruct);

    //    // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
    //    IntPtr buff = Marshal.AllocHGlobal(size);
    //    // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
    //    Marshal.Copy(data, 0, buff, size);
    //    // 복사된 데이터를 구조체 객체로 변환한다.
    //    Marshal.PtrToStructure(buff, myStruct);
    //    // 비관리 메모리 영역에 할당했던 메모리를 해제한다.
    //    Marshal.FreeHGlobal(buff);

    //    // 구조체와 원래의 데이터의 크기 비교.
    //    if (Marshal.SizeOf(myStruct) != data.Length)
    //        return null; // 크기가 다르면 null 반환.

    //    // 구조체 리턴
    //    return myStruct;
    //}
}
