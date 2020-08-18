using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource Pool Manager랑은 다름. 이 클래스는 단순한 테이블 클래스이고 리소스 풀은 해당 씬에 필요한 기초정보만 들고 있음.
// 또한 리소스 풀은 생성이 많이 필요한 오브젝트의 수를 한정시켜 최적화를 시킬 수 있고
// 더 최적화를 시키려면 각 씬마다 리소스풀을 가지고 있게 하여 해당 씬에서만 필요한 데이터를 가지고 있게 한다.
// 나중에 쓸거면 TableManager라고 하기

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
        Load<Dialog>(FileName.Dialog);
        Load<Scenario>(FileName.Scenario);
    }

    void Load<T>(FileName name) where T : TableBase, new()
    {
        T temp = new T();

        if (!_jsonFiles.ContainsKey(name.ToString()))
        {
            TextAsset ta = Resources.Load("bin/" + name.ToString()) as TextAsset;
            temp.LoadTable(ta.text);
            _jsonFiles.Add(name.ToString(), temp);
        }
    }

    public TableBase GetTable(string tableName)
    {
        return _jsonFiles[tableName];
    }
}