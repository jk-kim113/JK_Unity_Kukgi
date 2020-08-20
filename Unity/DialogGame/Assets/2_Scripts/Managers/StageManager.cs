using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static StageManager _uniqueInstance;
    public static StageManager _instance { get { return _uniqueInstance; } }

    int _storyIndex;

    float _timeCheck;
    float _autoNext = 3.0f;

    bool _isCount;
    public bool _isCounting { set { _isCount = value; } }

    private void Awake()
    {
        _uniqueInstance = this;
        _isCount = false;
    }

    private void Start()
    {
        InitSetting();
    }

    private void Update()
    {
        if(_isCount)
        {
            _timeCheck += Time.deltaTime;
            if (_timeCheck >= _autoNext)
            {
                _timeCheck = 0;
                _isCount = false;
                NextStep();
            }
        }   
    }

    void InitSetting()
    {
        StageUIManager._instance.SetEpisode(SaveDataManager._instance._nowEpi.ToString());

        int index = 10 * (SaveDataManager._instance._nowEpi - 1) + SaveDataManager._instance._nowStage;
        StageUIManager._instance.SetBG(ResourcePoolManager._instance.GetImage(
                                        TableManager._instance.Get(eTableType.Scenario).ToS(index, "ImageName")));

        _storyIndex = TableManager._instance.Get(eTableType.Scenario).ToI(index, "StartIndex");

        NextStep();
    }

    void NextStep()
    {
        TableBase tb = TableManager._instance.Get(eTableType.Dialog);
        
        StageUIManager._instance.SetCharacter(ResourcePoolManager._instance.GetImage(tb._datas[_storyIndex.ToString()]["ImageName"]));
        StageUIManager._instance.SetName(string.Format(tb._datas[_storyIndex.ToString()]["Name"], "홍길동"));

        switch(tb._datas[_storyIndex.ToString()]["SentencesPosition"])
        {
            case "0":
                StageUIManager._instance.SetNarration(string.Format(tb._datas[_storyIndex.ToString()]["Sentences"], "홍길동"));
                break;
            case "1":
                StageUIManager._instance.SetStory(string.Format(tb._datas[_storyIndex.ToString()]["Sentences"], "홍길동"));
                break;
        }

        _storyIndex++;
    }
}
