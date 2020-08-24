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

    int _playerWin = 0;
    int _comWin = 0;
    int _playCount = 5;

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
            GameStart();
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
            StageUIManager._instance.SetCharacter(null);
            GameStart();
            return;
        }

        StageUIManager._instance.SetCharacter(ResourcePoolManager._instance.GetImage(tb._datas[_storyIndex.ToString()]["ImageName"]));
        StageUIManager._instance.SetName(string.Format(tb._datas[_storyIndex.ToString()]["Name"], SaveDataManager._instance._nowSaveData._playerName));

        WriteSentences(tb, _storyIndex, false);

        _storyIndex++;
    }

    void GameStart()
    {
        StageUIManager._instance.SetGameMode(true);
    }

    public void SendRSPType(RSPButton.eRSPType type)
    {
        StartCoroutine(RSP(type));
    }

    IEnumerator RSP(RSPButton.eRSPType type)
    {
        int com = Random.Range(0, 3);

        StageUIManager._instance.SettingResult((int)type, com);
        _playCount--;

        switch (type)
        {
            case RSPButton.eRSPType.Rock:
                if (com == 1)
                    _playerWin++;
                else if (com == 2)
                    _comWin++;
                else
                    _playCount++;
                break;
            case RSPButton.eRSPType.Scissor:
                if (com == 0)
                    _comWin++;
                else if (com == 2)
                    _playerWin++;
                else
                    _playCount++;
                break;
            case RSPButton.eRSPType.Paper:
                if (com == 0)
                    _playerWin++;
                else if (com == 1)
                    _comWin++;
                else
                    _playCount++;
                break;
        }

        yield return new WaitForSeconds(1f);

        if (_playerWin >= 3)
        {
            StageUIManager._instance.SetGameMode(false);
            StageUIManager._instance.SetNotification("해당 스테이지의 스토리가 끝났습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
            SaveDataManager._instance.NextStage();
        }
        else if (_comWin >= 3)
        {
            StageUIManager._instance.SetGameMode(false);
            StageUIManager._instance.SetNotification("해당 스테이지의 스토리가 끝났습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
        }

        yield return new WaitForSeconds(1f);
        StageUIManager._instance.SetOriginGame();
    }

    void WriteSentences(TableBase tb, int index, bool isImmediate)
    {
        switch (tb._datas[index.ToString()]["SentencesPosition"])
        {
            case "0":
                StageUIManager._instance.SetNarration(string.Format(tb._datas[index.ToString()]["Sentences"], 
                                                SaveDataManager._instance._nowSaveData._playerName), isImmediate);
                break;
            case "1":
                StageUIManager._instance.SetStory(string.Format(tb._datas[index.ToString()]["Sentences"], 
                                                SaveDataManager._instance._nowSaveData._playerName), isImmediate);
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
