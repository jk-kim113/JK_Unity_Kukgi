using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtResult;
    [SerializeField]
    Text _txtSec;
    [SerializeField]
    Text _txtMSec;
    [SerializeField]
    Text _txtKillCount;
    [SerializeField]
    Text _txtLimitKillCount;
    [SerializeField]
    GameObject _rootWin;
    [SerializeField]
    GameObject _rootLose;
#pragma warning restore

    public void OpenWindow(bool isWin, float time, int kill, int limitKill)
    {
        _txtResult.text = (isWin) ? "WIN" : "LOSE";
        _rootWin.SetActive(isWin);
        _rootLose.SetActive(!isWin);

        _txtKillCount.text = kill.ToString();
        _txtLimitKillCount.text = limitKill.ToString();

        int sec = (int)time;
        int msec = (int)((time - sec) * 100);
        _txtSec.text = sec.ToString();
        _txtMSec.text = (msec < 10) ? "0" : string.Empty;
        _txtMSec.text += msec.ToString();
    }

    void ClickRestartButton()
    {
        SceneControlManager._instance.StartLoadIngameScene(UserInfoManager._instance._nowStageNum);
    }

    public void ClickHomeButton()
    {
        SceneControlManager._instance.StartLoadHomeScene();
    }

    public void ClickNextButton()
    {
        SceneControlManager._instance.StartLoadIngameScene(UserInfoManager._instance._nowStageNum + 1);
    }
}
