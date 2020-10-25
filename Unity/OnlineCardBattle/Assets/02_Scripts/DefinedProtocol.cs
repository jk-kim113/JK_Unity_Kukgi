using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinedProtocol
{
    public enum eFromClient
    {
        OverlapCheck_ID,
        JoinGame,

        LogIn,
        MyInfo,

        ChooseCard,
        CreateRoom,
        EnterRoom,
        ExitRoom,

        AddAI,

        Ready,
        GameStart,

        end
    }

    public enum eToClient
    {
        CompleteJoin,

        OverlapCheckResult_ID,
        LogInResult,

        ShowRoomInfo,
        ShowUserInfo,

        AfterCreateRoom,
        SuccessEnterRoom,
        FailEnterRoom,

        ShowExit,
        ShowMaster,
        ShowReady,
        CanPlay,

        GameStart,
        NextTurn,
        ChooseInfo,
        ChooseResult,

        GameResult,

        end
    }
}
