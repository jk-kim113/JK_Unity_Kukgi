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

    bool _isFinish;

    bool _isCount;
    public bool _isCounting { set { _isCount = value; } }

    private void Awake()
    {
        _uniqueInstance = this;
        _isCount = false;
        _isFinish = false;
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

        if(_storyIndex == 0)
        {
            _isFinish = true;
            StageUIManager._instance.SetNotification("해당 스테이지는 스토리가 없습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
            return;
        }

        NextStep();
    }

    void NextStep()
    {
        TableBase tb = TableManager._instance.Get(eTableType.Dialog);
        
        if(tb.ToI(_storyIndex, "Paragraph") != SaveDataManager._instance._nowStage)
        {
            _isFinish = true;
            StageUIManager._instance.SetNotification("해당 스테이지의 스토리가 끝났습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
            return;
        }

        StageUIManager._instance.SetCharacter(ResourcePoolManager._instance.GetImage(tb._datas[_storyIndex.ToString()]["ImageName"]));
        StageUIManager._instance.SetName(string.Format(tb._datas[_storyIndex.ToString()]["Name"], "홍길동"));

        WriteSentences(tb, _storyIndex, false);

        _storyIndex++;
    }

    void WriteSentences(TableBase tb, int index, bool isImmediate)
    {
        switch (tb._datas[index.ToString()]["SentencesPosition"])
        {
            case "0":
                StageUIManager._instance.SetNarration(string.Format(tb._datas[index.ToString()]["Sentences"], "홍길동"), isImmediate);
                break;
            case "1":
                StageUIManager._instance.SetStory(string.Format(tb._datas[index.ToString()]["Sentences"], "홍길동"), isImmediate);
                break;
        }
    }

    public void DownNextBtn()
    {
        if (_isFinish)
            return;

        if(!_isCount)
        {
            StageUIManager._instance.StopWrite();
            WriteSentences(TableManager._instance.Get(eTableType.Dialog), _storyIndex - 1, true);
            _isCount = true;
        }
        else
        {
            _timeCheck = 0;
            _isCount = false;
            NextStep();
        }
    }
}
