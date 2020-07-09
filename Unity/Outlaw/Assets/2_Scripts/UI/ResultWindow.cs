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
    GameObject _rootWin;
    [SerializeField]
    GameObject _rootLose;
#pragma warning  restore

    public void OpenWindow(bool isWin)
    {
        _txtResult.text = (isWin) ? "성공" : "실패";
        _rootWin.gameObject.SetActive(isWin);
        _rootLose.gameObject.SetActive(!isWin);
    }

    public void ClickRestartButton()
    {
        SceneControlManager._instance.StartSceneIngame(SceneControlManager._instance._curStageNum);
    }

    public void ClickHomeButton()
    {
        SceneControlManager._instance.StartSceneLobby();
    }
}
