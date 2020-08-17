using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public enum FileName
    {
        Dialog,
        Scenario,

        max
    }

    Dictionary<string, TableBase> _jsonFiles = new Dictionary<string, TableBase>();

    static JsonManager _uniqueInstance;
    public static JsonManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        LoadAllJsonFile();
    }

    public void LoadAllJsonFile()
    {   
        Dialog _dialog = new Dialog();
        Scenario _scenario = new Scenario();

        _jsonFiles.Add("Dialog", _dialog);
        _jsonFiles.Add("Scenario", _scenario);

        foreach(string key in _jsonFiles.Keys)
        {
            TextAsset ta = Resources.Load("bin/" + key) as TextAsset;
            _jsonFiles[key].LoadTable(ta.text);
        }
    }

    public string GetToStr(string tableName, int index, string columnName)
    {
        return _jsonFiles[tableName].GetToStr(index, columnName);
    }
}
