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

    int _playerWin = 0;
    int _comWin = 0;
    int _allowedLoseCnt;
    int _playCount;
    int _currentPlayCount;
    int _countMonster;
    int _currentMonCnt;
    int _nowMonIndex;
    List<int> _monsterIndexList = new List<int>();

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
                SetDialog();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            GameObject eff = EffectManager._instance.GetEffect(EffectManager.eKindEffect.MouseClick);
            eff.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void InitSetting()
    {
        UIManager._instance.Close(UIManager.eKindWindow.LobbyUI);

        int index = 10 * (SaveDataManager._instance._nowEpi - 1) + SaveDataManager._instance._nowStage;

        TableBase scenario = TableManager._instance.Get(eTableType.Scenario);

        StageBGUI sBGUI = UIManager._instance.OpenWnd<StageBGUI>(UIManager.eKindWindow.StageBGUI);
        sBGUI.InitSetting(ResourcePoolManager._instance.GetImage(scenario.ToS(index, "ImageName")),
                            SaveDataManager._instance._nowEpi.ToString());

        StageUI sUI = UIManager._instance.OpenWnd<StageUI>(UIManager.eKindWindow.StageUI);

        _storyIndex = scenario.ToI(index, "StartIndex");
        if(_storyIndex == 0)
        {
            GameStart();
            return;
        }

        SetDialog();
    }

    void SetDialog()
    {
        TableBase tb = TableManager._instance.Get(eTableType.Dialog);
        
        if(tb.ToI(_storyIndex, "Paragraph") != SaveDataManager._instance._nowStage)
        {
            GameStart();
            return;
        }

        StageUI sUI = UIManager._instance.GetWnd<StageUI>(UIManager.eKindWindow.StageUI);

        switch (tb._datas[_storyIndex.ToString()]["SentencesPosition"])
        {
            case "0":
                sUI.SetDialog(null, true, 
                    string.Format(tb._datas[_storyIndex.ToString()]["Sentences"], SaveDataManager._instance._nowSaveData._playerName));
                break;
            case "1":
                sUI.SetDialog(ResourcePoolManager._instance.GetImage(tb._datas[_storyIndex.ToString()]["ImageName"]), false,
                    string.Format(tb._datas[_storyIndex.ToString()]["Name"], SaveDataManager._instance._nowSaveData._playerName),
                    string.Format(tb._datas[_storyIndex.ToString()]["Sentences"], SaveDataManager._instance._nowSaveData._playerName));
                break;
        }

        _storyIndex++;
    }

    void GameStart()
    {
        UIManager._instance.Close(UIManager.eKindWindow.StageUI);

        MiniGameUI mgUI = UIManager._instance.OpenWnd<MiniGameUI>(UIManager.eKindWindow.MiniGameUI);

        _currentMonCnt = 1;
        _countMonster = (int)(SaveDataManager._instance._nowStage * 0.5 + 1);

        _allowedLoseCnt = TableManager._instance.Get(eTableType.PlayerLevelTable).ToI(SaveDataManager._instance._nowLevel, "LoseCount");
        mgUI.SetAllowedLose(_allowedLoseCnt);
        mgUI.SetPlayerLevel(SaveDataManager._instance._nowLevel);

        TableBase tb = TableManager._instance.Get(eTableType.MonsterTable);
        foreach(string key in tb._datas.Keys)
        {
            if(SaveDataManager._instance._nowEpi >= int.Parse(tb._datas[key]["ActivateEpi"])
                && SaveDataManager._instance._nowEpi <= int.Parse(tb._datas[key]["InactivateEpi"]))
            {
                _monsterIndexList.Add(int.Parse(key));
            }
        }

        SetMonster(tb, mgUI);

        mgUI.SetWinLoseCount(0, 0);
        mgUI.SetMonsterCount(_currentMonCnt, _countMonster);
    }

    void SetMonster(TableBase tb, MiniGameUI mgUI)
    {
        int id = Random.Range(0, _monsterIndexList.Count);
        _nowMonIndex = _monsterIndexList[id];

        _currentPlayCount = 0;
        _playCount = tb.ToI(_nowMonIndex, "GameCount");

        mgUI.SetMonsterName(tb.ToS(_nowMonIndex, "Name"));
        mgUI.SetGameCondition((_playCount / 2 + 1));
        mgUI.SetGameCount(_currentPlayCount, _playCount);
    }

    public void SendRSPType(RSPButton.eRSPType type)
    {
        StartCoroutine(RSP(type));
    }

    IEnumerator RSP(RSPButton.eRSPType type)
    {
        int com = ComResult();

        MiniGameUI mgUI = UIManager._instance.GetWnd<MiniGameUI>(UIManager.eKindWindow.MiniGameUI);
        mgUI.SettingResult((int)type, com);
        _currentPlayCount++;

        switch (type)
        {
            case RSPButton.eRSPType.Rock:
                if (com == 1)
                    _playerWin++;
                else if (com == 2)
                    _comWin++;
                else
                    _currentPlayCount--;
                break;
            case RSPButton.eRSPType.Scissor:
                if (com == 0)
                    _comWin++;
                else if (com == 2)
                    _playerWin++;
                else
                    _currentPlayCount--;
                break;
            case RSPButton.eRSPType.Paper:
                if (com == 0)
                    _playerWin++;
                else if (com == 1)
                    _comWin++;
                else
                    _currentPlayCount--;
                break;
        }

        mgUI.SetGameCount(_currentPlayCount, _playCount);

        yield return new WaitForSeconds(1f);

        if (_playerWin >= (_playCount / 2 + 1))
        {
            SaveDataManager._instance._nowExp++;
            int exp = SaveDataManager._instance._nowExp -
                TableManager._instance.Get(eTableType.PlayerLevelTable).ToI(SaveDataManager._instance._nowLevel, "LevelUpCount");
            if (exp >= 0)
            {
                SaveDataManager._instance._nowLevel++;
                SaveDataManager._instance._nowExp = exp;
                UIManager._instance.GetWnd<MiniGameUI>(UIManager.eKindWindow.MiniGameUI).SetPlayerLevel(SaveDataManager._instance._nowLevel);
            }

            if (++_currentMonCnt > _countMonster)
            {
                GameObject eff = EffectManager._instance.GetEffect(EffectManager.eKindEffect.MiniGameWin);
                eff.transform.position = Vector3.zero;

                SaveDataManager._instance.NextStage();
                UIManager._instance.Close(UIManager.eKindWindow.MiniGameUI);
                UIManager._instance.Open(UIManager.eKindWindow.StageUI);
                UIManager._instance.GetWnd<StageUI>(UIManager.eKindWindow.StageUI).SetNotification(
                                "해당 스테이지의 스토리가 끝났습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
            }
            else
            {
                mgUI.SetMonsterCount(_currentMonCnt, _countMonster);
                SetMonster(TableManager._instance.Get(eTableType.MonsterTable), UIManager._instance.GetWnd<MiniGameUI>(UIManager.eKindWindow.MiniGameUI));
                _playerWin = _comWin = 0;
                mgUI.SetWinLoseCount(_playerWin, _comWin);
            }
        }
        else if (_comWin > _allowedLoseCnt)
        {
            GameObject eff = EffectManager._instance.GetEffect(EffectManager.eKindEffect.MiniGameLose);
            eff.transform.position = Vector3.zero;

            UIManager._instance.Close(UIManager.eKindWindow.MiniGameUI);
            UIManager._instance.Open(UIManager.eKindWindow.StageUI);
            UIManager._instance.GetWnd<StageUI>(UIManager.eKindWindow.StageUI).SetNotification(
                                "해당 스테이지의 스토리가 끝났습니다. 닫기 버튼을 이용하여 로비로 돌아가십시오.");
        }

        yield return new WaitForSeconds(1f);
        mgUI.SetWinLoseCount(_playerWin, _comWin);
        mgUI.SetOriginGame();
    }

    int ComResult()
    {
        int temp = Random.Range(1, 101);

        TableBase monTb = TableManager._instance.Get(eTableType.MonsterTable);

        if (temp < monTb.ToI(_nowMonIndex, "R_Rate"))
            return 0;
        else if (temp < monTb.ToI(_nowMonIndex, "R_Rate") + monTb.ToI(_nowMonIndex, "S_Rate"))
            return 1;
        else
            return 2;
    }

    public void DownNextBtn()
    {
        if(!_isCount)
        {
            TableBase tb = TableManager._instance.Get(eTableType.Dialog);
            StageUI sUI = UIManager._instance.GetWnd<StageUI>(UIManager.eKindWindow.StageUI);
            switch (tb._datas[(_storyIndex - 1).ToString()]["SentencesPosition"])
            {
                case "0":
                    sUI.WriteImmediately(true);
                    break;
                case "1":
                    sUI.WriteImmediately(false);
                    break;
            }
            _isCount = true;
        }
        else
        {
            _timeCheck = 0;
            _isCount = false;
            SetDialog();
        }
    }
}
