using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject[] _player;
    [SerializeField]
    GameObject[] _com;
    [SerializeField]
    Text _countWinLose;
    [SerializeField]
    Text _countMonster;
#pragma warning restore

    public void SettingResult(int player, int com)
    {
        for (int n = 0; n < _player.Length; n++)
        {
            _player[n].SetActive(false);
            _com[n].SetActive(false);
        }

        _player[player].SetActive(true);
        _com[com].SetActive(true);
    }

    public void SetWinLoseCount(int win, int lose)
    {
        _countWinLose.text = win.ToString() + "승 " + lose.ToString() + "패";
    }

    public void SetMonsterCount(int moncnt, int total)
    {
        _countMonster.text = moncnt.ToString() + " / " + total.ToString();
    }

    public void SetOriginGame()
    {
        for (int n = 0; n < _player.Length; n++)
        {
            _player[n].SetActive(true);
            _com[n].SetActive(true);
        }
    }
}
