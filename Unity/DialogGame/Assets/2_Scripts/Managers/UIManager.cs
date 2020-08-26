using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : TSingleton<UIManager>
{
    public enum eKindWindow
    {
        StartUI,
        LobbyUI,
        StageUI,
        StageBGUI,
        MiniGameUI,

        max
    }

    Dictionary<eKindWindow, GameObject> _dicUIs = new Dictionary<eKindWindow, GameObject>();

    protected override void Init()
    {
        base.Init();
    }

    public bool isOpened(eKindWindow wnd)
    {
        if(_dicUIs.ContainsKey(wnd))
            return _dicUIs[wnd].activeSelf;

        return false;
    }

    public void DeleteKey(eKindWindow wnd)
    {
        if (_dicUIs.ContainsKey(wnd))
            _dicUIs.Remove(wnd);
    }

    public T GetWnd<T>(eKindWindow wnd) where T : Component
    {
        if (_dicUIs.ContainsKey(wnd))
            return _dicUIs[wnd].GetComponent<T>();
        
        return null;
    }

    public void Open(eKindWindow wnd)
    {
        if(_dicUIs.ContainsKey(wnd))
        {
            _dicUIs[wnd].SetActive(true);
        }
        else
        {
            GameObject ui = Instantiate(ResourcePoolManager._instance.GetPrefab(wnd.ToString()));
            _dicUIs.Add(wnd, ui);
        }
    }

    public T OpenWnd<T>(eKindWindow wnd) where T : Component
    {
        if (!_dicUIs.ContainsKey(wnd))
        {
            GameObject ui = Instantiate(ResourcePoolManager._instance.GetPrefab(wnd.ToString()), transform);
            _dicUIs.Add(wnd, ui);
            return ui.GetComponent<T>();
        }
        else
        {
            if(!_dicUIs[wnd].activeSelf)
                _dicUIs[wnd].SetActive(true);

            return GetWnd<T>(wnd);
        }
    }

    public void Close(eKindWindow wnd)
    {
        if (_dicUIs.ContainsKey(wnd))
        {
            _dicUIs[wnd].SetActive(false);
        }
    }
}
