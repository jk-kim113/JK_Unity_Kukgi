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
    [SerializeField]
    Text _countGame;
    [SerializeField]
    Text _gameCondition;
    [SerializeField]
    Text _monName;
    [SerializeField]
    Text _allowedLose;
    [SerializeField]
    Text _playerLevel;
#pragma warning restore

    private void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

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

    public void SetGameCount(int current, int max)
    {
        _countGame.text = string.Format("판 수 : {0} / {1}", current, max);
    }

    public void SetGameCondition(int value)
    {
        _gameCondition.text = string.Format("{0}번 이기면 승리", value);
    }

    public void SetMonsterName(string name)
    {
        _monName.text = string.Format("Mon Name : {0}", name);
    }

    public void SetAllowedLose(int value)
    {
        _allowedLose.text = string.Format("{0}번은 져도 됨", value);
    }

    public void SetPlayerLevel(int level)
    {
        _playerLevel.text = string.Format("Player Level : {0}", level);
    }
}
