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

    public void GoStage()
    {
        SaveDataManager._instance.SettingStageInfo(LobyUIManager._instance._nowEpi, LobyUIManager._instance._nowStage);
        SceneManager.LoadScene("StageScene");
    }
}
