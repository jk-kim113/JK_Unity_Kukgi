using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    AvatarInfo _avatarInfo;
    double _totalGold;
    double _totalStar;
    int _completeStage;
    int _currentStage;

    public UserInfo(AvatarInfo avInfo, double totalgold, double totalstar, int completeStage, int curStage)
    {
        _avatarInfo = avInfo;
        _totalGold = totalgold;
        _totalStar = totalstar;
        _completeStage = completeStage;
        _currentStage = curStage;
    }
}
