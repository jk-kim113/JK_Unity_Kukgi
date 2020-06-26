using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageInfo 
{
    public int _no;
    public string _name;
    public int _limitCardCount;
    public int _limitMonsterCount;
    public List<int> _ltMonsterIndexes;
    public int _deathCountForTrophy;
    public int _limitTimeForTrophy;
    public int _limitCardActionCountForTrophy;

    public StageInfo(int no, int cardCount, int monsterCount, string name, params int[] monIdx)
    {
        _ltMonsterIndexes = new List<int>();
        _no = no;
        _name = name;
        _limitCardCount = cardCount;
        _limitMonsterCount = monsterCount;
        for (int n = 0; n < monIdx.Length; n++)
            _ltMonsterIndexes.Add(monIdx[n]);
        _deathCountForTrophy = 1;
        _limitTimeForTrophy = _no * 2 + 8;
        _limitCardActionCountForTrophy = _no * 20 + 20;
    }
}
