using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : MonoBehaviour
{
    Dictionary<int, MonsterInfo> _dicMonsterInfos;
    Dictionary<int, StageInfo> _dicStageInfos;

    static ResourcePoolManager _uniqueInstance;
    public static ResourcePoolManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        _dicMonsterInfos = new Dictionary<int, MonsterInfo>();
        _dicStageInfos = new Dictionary<int, StageInfo>();

        MonsterDataSetup();
        StageDataSetup();
    }

    void MonsterDataSetup()
    {
        MonsterInfo mI = new MonsterInfo("젤다", 2, 0, 5, 30.0f, 50.0f, BaseStatus.eTypeRank.Shallow, 
            BaseStatus.eTypeCharacter.Lazy, MonsterInfo.eTypeShape.EFREET);
        _dicMonsterInfos.Add(1, mI);
        mI = new MonsterInfo("링크", 4, 1, 17, 35.0f, 32.0f, BaseStatus.eTypeRank.Normal, 
            BaseStatus.eTypeCharacter.Fool, MonsterInfo.eTypeShape.GNOME);
        _dicMonsterInfos.Add(2, mI);
        mI = new MonsterInfo("가논", 6, 2, 11, 40.0f, 34.0f, BaseStatus.eTypeRank.Elite, 
            BaseStatus.eTypeCharacter.Normal, MonsterInfo.eTypeShape.JINN);
        _dicMonsterInfos.Add(3, mI);
        mI = new MonsterInfo("하이랄", 8, 3, 14, 45.0f, 36.0f, BaseStatus.eTypeRank.RoyalElite, 
            BaseStatus.eTypeCharacter.Impatient, MonsterInfo.eTypeShape.UNDINE);
        _dicMonsterInfos.Add(4, mI);
        mI = new MonsterInfo("카카리코", 10, 4, 17, 50.0f, 38.0f, BaseStatus.eTypeRank.Boss, 
            BaseStatus.eTypeCharacter.Ferocious, MonsterInfo.eTypeShape.EFREET);
        _dicMonsterInfos.Add(5, mI);
    }

    void StageDataSetup()
    {
        StageInfo sI = new StageInfo(1, 12, 2, "시작의 대지", 1, 2);
        _dicStageInfos.Add(1, sI);

        sI = new StageInfo(2, 24, 3, "2번째 맵", 1, 2, 3);
        _dicStageInfos.Add(2, sI);

        sI = new StageInfo(3, 30, 5, "3번째 맵", 1, 2, 3, 4, 5);
        _dicStageInfos.Add(3, sI);
    }

    public StageInfo GetStageInfo(int stageNumber)
    {
        if (_dicStageInfos.ContainsKey(stageNumber))
            return _dicStageInfos[stageNumber];
        else
            return new StageInfo();
    }

    public MonsterInfo GetMonsterInfo(int monsterIdx)
    {
        if (_dicMonsterInfos.ContainsKey(monsterIdx))
            return _dicMonsterInfos[monsterIdx];
        else
            return new MonsterInfo();
    }

}
