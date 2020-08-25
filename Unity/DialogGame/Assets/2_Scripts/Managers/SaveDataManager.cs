using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveDataManager : TSingleton<SaveDataManager>
{
    SaveData _saveData;
    public SaveData _nowSaveData { get { return _saveData; } }

    int _currentEpi;
    int _currentStage;

    public int _nowEpi { get { return _currentEpi; } }
    public int _nowStage { get { return _currentStage; } }

    protected override void Init()
    {
        base.Init();
        //PlayerPrefs.DeleteAll();
    }

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, _saveData);

        string data = Convert.ToBase64String(stream.GetBuffer());
        
        PlayerPrefs.SetString("GameData", data);
        stream.Close();
    }

    public bool LoadData()
    {
        string data = PlayerPrefs.GetString("GameData");

        if (!string.IsNullOrEmpty(data))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));

            _saveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            return true;
        }
        else
        {
            _saveData._playerName = string.Empty;
            _saveData._currentEpi = 1;
            _saveData._currentStage = 0;
            _saveData._countExp = 0;

            return false;
        }
    }

    public void SetPlayerName(string name)
    {
        _saveData._playerName = name;
        SceneManager.LoadScene("LobbyScene");
    }

    public void SettingStageInfo(int epi, int stage)
    {
        _currentEpi = epi;
        _currentStage = stage;
    }

    public void NextStage()
    {
        if (_currentEpi != _saveData._currentEpi || _currentStage != (_saveData._currentStage + 1))
            return;
        
        if (_currentStage == 10)
        {
            _saveData._currentEpi++;
            _saveData._currentStage = 0;
        }
        else
        {
            _saveData._currentStage = _currentStage;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
