using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct SaveData
{
    public string _playerName;
    public int _currentEpi;
    public int _currentStage;
    public int _currentLevel;
    public int _currentExp;

    public SaveData(string playerName, int nowEpi, int nowStage, int level, int exp)
    {
        _playerName = playerName;
        _currentEpi = nowEpi;
        _currentStage = nowStage;
        _currentLevel = level;
        _currentExp = exp;
    }
}
