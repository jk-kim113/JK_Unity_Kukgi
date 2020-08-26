using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    static StartManager _uniqueInstance;
    public static StartManager _instance { get { return _uniqueInstance; } }

    InputField _enterName;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        if (!UIManager._instance.isOpened(UIManager.eKindWindow.StartUI))
        {
            UIManager._instance.Open(UIManager.eKindWindow.StartUI);
            _enterName = GameObject.FindGameObjectWithTag("PlayerNameField").GetComponent<InputField>();
        }   

        if (!SaveDataManager._instance.LoadData())
            _enterName.text = string.Empty;
        else
            _enterName.text = SaveDataManager._instance._nowSaveData._playerName;
    }

    public void SetPlayerName()
    {
        SaveDataManager._instance.SetPlayerName(_enterName.text);
    }
}
