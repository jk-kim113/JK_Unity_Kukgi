using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : TSingleton<SaveDataManager>
{
    int _currentEpisode;
    int _currentStage;

    public int _nowEpi { get { return _currentEpisode; } }
    public int _nowStage { get { return _currentStage; } }

    protected override void Init()
    {
        base.Init();
    }

    public void SettingStageInfo(int epi, int stage)
    {
        _currentEpisode = epi;
        _currentStage = stage;
    }
}
