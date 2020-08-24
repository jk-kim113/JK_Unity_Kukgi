using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _enterName;
#pragma warning restore

    static StartManager _uniqueInstance;
    public static StartManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
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
