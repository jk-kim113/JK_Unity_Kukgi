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
        UIManager._instance.DeleteKey(UIManager.eKindWindow.StageUI);
        UIManager._instance.DeleteKey(UIManager.eKindWindow.StageBGUI);
        UIManager._instance.DeleteKey(UIManager.eKindWindow.MiniGameUI);

        if (!UIManager._instance.isOpened(UIManager.eKindWindow.LobbyUI))
            UIManager._instance.Open(UIManager.eKindWindow.LobbyUI);
    }

    public void GoStage()
    {
        SaveDataManager._instance.SettingStageInfo(LobyUIManager._instance._nowEpi, LobyUIManager._instance._nowStage);
        SceneManager.LoadScene("StageScene");
    }
}
