using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    static LobbyManager _uniqueInstance;
    public static LobbyManager _instance { get { return _uniqueInstance; } }


    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        UIManager._instance.Close(UIManager.eKindWindow.StartUI);
        UIManager._instance.Close(UIManager.eKindWindow.StageBGUI);
        UIManager._instance.Close(UIManager.eKindWindow.StageUI);
        UIManager._instance.Close(UIManager.eKindWindow.MiniGameUI);

        UIManager._instance.OpenWnd<LobyUIManager>(UIManager.eKindWindow.LobbyUI);
    }

    public void GoStage()
    {
        SaveDataManager._instance.SettingStageInfo(LobyUIManager._instance._nowEpi, LobyUIManager._instance._nowStage);
        SceneManager.LoadScene("StageScene");
    }
}
