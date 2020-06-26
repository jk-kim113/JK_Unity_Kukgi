using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtExplan;
#pragma warning restore

    int _selecetStageNum;

    public void OpenWindow(int stageNum)
    {
        gameObject.SetActive(true);
        _selecetStageNum = stageNum;
        _txtExplan.text = stageNum.ToString() + "스테이지를 플레이하시겠습니까?";
    }

    public void StageYesButton()
    {
        SceneControlManager._instance.StartLoadIngameScene(_selecetStageNum);
    }

    public void StageNoButton()
    {
        gameObject.SetActive(false);
    }
}
