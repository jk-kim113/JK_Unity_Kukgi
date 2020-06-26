using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    int _playStageNumber = 0;
    int _clearStageNumber  = 0;

    static UserInfoManager _uniqueInstance;
    public static UserInfoManager _instance { get { return _uniqueInstance; } }

    List<int> _ltClearTrophyCounts;
    public int _nowStageNum { get { return _playStageNumber; } set { _playStageNumber = value; } }
    public int _clearStage { get { return _clearStageNumber; } set { _clearStageNumber = value; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        _ltClearTrophyCounts = new List<int>();
    }

    public void SetClearStageInfo(int stageNum, int trophyCount)
    {
        if (_clearStageNumber < stageNum)
            _clearStageNumber = stageNum;

        if (stageNum - 1 >= _ltClearTrophyCounts.Count)
            _ltClearTrophyCounts.Add(trophyCount);
        else
            _ltClearTrophyCounts[trophyCount - 1] = trophyCount;
    }

    public int GetTrophyCount(int stageNum)
    {
        return _ltClearTrophyCounts[stageNum - 1];
    }
}
