using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public enum eTypeWindow
    {
        StageWND      = 0,
        PlayerWND
    }

    static LobbyManager _uniqueInstance;
    public static LobbyManager _instance { get { return _uniqueInstance; } }

    GameObject _prefabStageWindow;
    StageWindow _wndStage;

    private void Awake()
    {
        _uniqueInstance = this;

        _prefabStageWindow = Resources.Load("Prefabs/UI/StageWindow") as GameObject;
    }

    public void OpenWindow(eTypeWindow type)
    {
        GameObject go;
        switch(type)
        {
            case eTypeWindow.StageWND:
                if(_wndStage == null)
                {
                    go = Instantiate(_prefabStageWindow);
                    _wndStage = go.GetComponent<StageWindow>();
                    _wndStage.OpenWindow();
                }
                else
                {
                    if (_wndStage.gameObject.activeSelf)
                        _wndStage.CloseWnd();
                    else
                        _wndStage.OpenWindow();
                }
                break;
            case eTypeWindow.PlayerWND:
                break;
        }
    }
}
