using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinedProtocol
{
    public enum eFromClient
    {
        Connect,

        ChooseCard,
        CreateRoom,
        EnterRoom,

        end
    }

    public enum eToClient
    {
        CheckConnect,
        ShowRoomInfo,
        ShowUserInfo,

        AfterCreateRoom,
        SuccessEnterRoom,
        FailEnterRoom,

        GameStart,
        NextTurn,
        ChooseInfo,
        ChooseResult,

        GameResult,

        end
    }
}
