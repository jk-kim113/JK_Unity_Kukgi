using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    public enum eTypeGameState
    {
        none        = 0,
        InitData,
        SettingScreenBG,
        PlayerAppearance,
        MonsterAppearance,
        CreateCard,
        GameStart,
        GamePlay,
        ReCreateCard,
        MonsterReAppearance,
        LockCard,
        EndGame,
        ResultGame,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    GameObject _prefabCard;
    [SerializeField]
    Sprite[] _iconSprites;
    [SerializeField]
    GameObject _prefabAvatar;
    [SerializeField]
    GameObject[] _prefabMonsters;
    [SerializeField]
    GameObject _prefabResultWnd;
    [SerializeField]
    GameObject _prefabExitWnd;

    [SerializeField]
    Slider _sliderFever;
    [SerializeField]
    float _autoChargeFever;
    [SerializeField]
    GameObject _scrollFeverBG;
    [SerializeField]
    float _actionChargeFever;
    [SerializeField]
    float _timeFeverEffect;
#pragma warning restore

    int _feverEff = 0;
    float _timeFeverCheck;

    // Ingame에서 사용되는 UI클래스.
    InfoMessage _infoMessageBox;
    MiniStatus _playerMiniWnd;
    MiniStatus _monsterMiniWnd;
    TimerWnd _timerWnd;
    StageInfoWnd _infoStageWnd;
    ExitWindow _wndExit;
    //===========================

    // 해당 Stage에서 사용할 정보
    int _nowStageNumber;
    float _timeGamePlay = 0;
    int _limitMonsterCount;
    int _cardCount;
    int _nextMonsterIndex = 0;
    List<MonsterInfo> _ltStageMonsterInfo = new List<MonsterInfo>();

    int _playCount = 0;
    //===========================
    SpriteRenderer _battleMap;
    float _timeCheck = 0;
    GameObject _spawnPointAvatar;
    AvatarControl _ctrlAvatar;
    GameObject _spawnPointMonster;
    MonsterControl _nowMonsterCtrl;

    const int _maxCardHorizontalCount = 6;
    Transform _positionCardRoot;
    List<Card> _ltCardInfos = new List<Card>();
    int[] _iconsIndexes;
    int _firstSelectCardNO = 0;
    int _secondSelectCardNO = 0;

    int _limitFailedCount = 3;
    int _nowFaliedCount = 0;
    bool _isSuccess;
    bool _isFever;

    eTypeGameState _currentGameState = eTypeGameState.none;
    public eTypeGameState _nowGameState { get { return _currentGameState; } }

    public bool _isCardAction { get { return _wndExit == null || (_wndExit != null && !_wndExit.gameObject.activeSelf); } }

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        GameObject go = GameObject.Find("CardRootPosition");
        _positionCardRoot = go.transform;

        go = GameObject.FindGameObjectWithTag("InfoMessageBox");
        _infoMessageBox = go.GetComponent<InfoMessage>();

        go = GameObject.FindGameObjectWithTag("Battlemap");
        _battleMap = go.GetComponent<SpriteRenderer>();

        _spawnPointAvatar = GameObject.Find("AvatarSpawnPoint");
        _spawnPointMonster = GameObject.Find("MonsterSpawnPoint");

        _playerMiniWnd = GameObject.FindGameObjectWithTag("PlayerMiniWnd").GetComponent<MiniStatus>();
        _playerMiniWnd.gameObject.SetActive(false);

        _monsterMiniWnd = GameObject.FindGameObjectWithTag("MonsterMiniWnd").GetComponent<MiniStatus>();
        _monsterMiniWnd.gameObject.SetActive(false);

        _timerWnd = GameObject.FindGameObjectWithTag("TimerWnd").GetComponent<TimerWnd>();

        _infoStageWnd = GameObject.FindGameObjectWithTag("StageInfoWnd").GetComponent<StageInfoWnd>();
    }

    private void Update()
    {
        switch(_currentGameState)
        {
            case eTypeGameState.none:
                if (SceneControlManager._instance._nowLoadingState == EnumClass.eStateLoadding.LoadingDone)
                    InitGameData();
                break;
            case eTypeGameState.InitData:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 1)
                    SettingGameBackground();
                break;
            case eTypeGameState.GameStart:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 0.5)
                {
                    StopCoroutine("CreateCardFromPosition");
                    GamePlay();
                }   
                break;
            case eTypeGameState.GamePlay:
                if(_isCardAction)
                {
                    if(!_isFever)
                    {
                        _sliderFever.value += _autoChargeFever * Time.deltaTime;
                        if (_sliderFever.value >= 1.0f)
                        {
                            _timeFeverCheck = _timeFeverEffect;
                            _isFever = true;
                            _scrollFeverBG.SetActive(true);
                            _sliderFever.value = 1.0f;
                        }
                    }
                    else
                    {
                        _sliderFever.value -= _autoChargeFever * Time.deltaTime;
                        if (_sliderFever.value <= 0.0f)
                        {
                            _isFever = false;
                            _scrollFeverBG.SetActive(false);
                            _sliderFever.value = 0.0f;
                        }

                        _timeFeverCheck -= Time.deltaTime;
                        if(_timeFeverCheck <= 0.0f)
                        {
                            _timeFeverCheck = _timeFeverEffect;
                            FeverEffect();
                        }
                    }
                    
                    _timeGamePlay += Time.deltaTime;
                    _timerWnd.SettingTime(_timeGamePlay);
                }
                break;
            case eTypeGameState.EndGame:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 2)
                    GameResult();
                break;
        }
    }

    private void LateUpdate() // 순서의 차이
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(_wndExit == null)
            {
                GameObject go = Instantiate(_prefabExitWnd);
                _wndExit = go.GetComponent<ExitWindow>();
                _wndExit.OpenWindow("게임을 종료 하시겠습니까?");
            }
            else
            {
                if(_wndExit.gameObject.activeSelf)
                {
                    _wndExit.gameObject.SetActive(false);
                }
                else
                {
                    _wndExit.OpenWindow("게임을 종료 하시겠습니까?");
                }
            }
        }
    }

    void FeverEffect()
    {
        int randomIndex = 0;
        int cardSetIndex = 0;
        
        do
        {
            randomIndex = Random.Range(0, _ltCardInfos.Count);
            cardSetIndex++;
            if (cardSetIndex > _ltCardInfos.Count)
                return;
        }
        while (_ltCardInfos[randomIndex]._isUsed || _ltCardInfos[randomIndex]._isFeverState 
                || _ltCardInfos[randomIndex]._isLockState || _ltCardInfos[randomIndex]._isCoupleLockState);


        for(int i = 0; i < _ltCardInfos.Count; i++)
        {
            if((_ltCardInfos[i]._ICON_INDEX == _ltCardInfos[randomIndex]._ICON_INDEX) && (randomIndex != i))
            {
                cardSetIndex = i;
                break;
            }
        }

        _ltCardInfos[randomIndex].FadeInFeverBack();
        _ltCardInfos[cardSetIndex].FadeInFeverBack();
    }

    void ChangeBattleMap(Sprite mapImg)
    {
        _battleMap.sprite = mapImg;
    }

    /// <summary>
    /// 카드 프리팹을 이용하여 카드를 화면상의 지정 위치에 등장하게 한다.
    /// </summary>
    private IEnumerator CreateCardFromPosition()
    {
        _iconsIndexes = new int[_cardCount];
        // 카드인덱스를 섞어서 iconsIndexes에 저장해둔다.

        for (int n = 0; n < _iconsIndexes.Length; n++)
        {
            _iconsIndexes[n] = n / 2;
        }
        
        for (int n = 0; n < _iconsIndexes.Length; n++)
        {
            int rid = Random.Range(0, _iconsIndexes.Length);
            int td = _iconsIndexes[n];
            _iconsIndexes[n] = _iconsIndexes[rid];
            _iconsIndexes[rid] = td;
        }

        yield return null;

        #region My Mix Card Code
        //List<int> ltIndex = new List<int>();
        //for (int i = 0; i < iconsIndexes.Length; i++)
        //{
        //    int temp = i % (iconsIndexes.Length / 2);
        //    ltIndex.Add(temp);
        //}


        //int randomIndex = 0;
        //int count = 0;
        //while(ltIndex.Count > 0)
        //{
        //    randomIndex = Random.Range(0, ltIndex.Count);
        //    iconsIndexes[count] = ltIndex[randomIndex];
        //    ltIndex.RemoveAt(randomIndex);
        //    count++;
        //}
        #endregion
        //===========================================

        for (int n = 0; n < _cardCount; n++)
        {
            int xid = n % _maxCardHorizontalCount;
            int yid = n / _maxCardHorizontalCount;
            Vector3 pos = new Vector3(_positionCardRoot.position.x + (2.4f * xid), _positionCardRoot.position.y - (3.5f * yid));
            Card card = Instantiate(_prefabCard, pos, Quaternion.identity, _positionCardRoot).GetComponent<Card>();
            card.InitData(n + 1, _iconsIndexes[n], _iconSprites[_iconsIndexes[n]]);
            _ltCardInfos.Add(card);

            yield return new WaitForSeconds(.1f);
        }

        if (_currentGameState == eTypeGameState.CreateCard)
            GameStart();
        else
            GamePlay();
    }

    public bool CheckSelectCard(int no)
    {
        if (_firstSelectCardNO == no)
            return false;
        else if (_secondSelectCardNO > 0)
            return false;
        else if (_firstSelectCardNO > 0)
        {   
            _secondSelectCardNO = no;
            StartCoroutine(CheckCard());
        }
        else
        {
            _firstSelectCardNO = no;
            StopCoroutine("CheckCard");
        }

        return true;
    }

    public IEnumerator CheckCard()
    {
        yield return new WaitForSeconds(.7f);

        _infoStageWnd.SetPlayerCount(++_playCount);

        if (_ltCardInfos[_firstSelectCardNO - 1]._ICON_INDEX != _ltCardInfos[_secondSelectCardNO - 1]._ICON_INDEX)
        {
            _ltCardInfos[_firstSelectCardNO - 1].EndMarking(false);
            yield return new WaitForSeconds(.2f);
            _ltCardInfos[_secondSelectCardNO - 1].EndMarking(false);
            _nowFaliedCount++;
            _infoStageWnd.SetUntilDamageCount(_nowFaliedCount, _limitFailedCount);
            _feverEff = 0;

            if (_nowFaliedCount >= _limitFailedCount)
            {
                _sliderFever.value -= _actionChargeFever * 0.7f;
                if (_sliderFever.value < 0)
                    _sliderFever.value = 0;
                _nowFaliedCount = 0;
                _nowMonsterCtrl.AttackAction();
            }
        }
        else
        {
            _ltCardInfos[_firstSelectCardNO - 1].EndMarking(true);
            _ltCardInfos[_secondSelectCardNO - 1].EndMarking(true);
            _ctrlAvatar.AttackAction();
            _feverEff++;
            if (_feverEff >= 3)
                _feverEff = 3;
            if(!_isFever)
                _sliderFever.value += _actionChargeFever * _feverEff;
        }

        _firstSelectCardNO = 0;
        _secondSelectCardNO = 0;

        yield return new WaitForSeconds(1f);
        if (!CheckMonsterAllDead(true))
        {
            if (CheckUsedAllCard())
            {   
                RecreateCard();
            }
        }
    }

    public void MonsterPostTreatment()
    {
        _monsterMiniWnd.gameObject.SetActive(false);
        if(CheckMonsterAllDead())
        {
            GameEnd(true);
        }
        else
        {
            MonsterReApperance();
        }
    }

    public void Battle(bool isMonster = false)
    {
        if(isMonster)
        {// 몬스터의 공격
            _ctrlAvatar.OnHitting(_nowMonsterCtrl.FinalDamage());
            _playerMiniWnd.SetHpRate(_ctrlAvatar._currentHPRate);
        }
        else
        {// 아바타의 공격
            _nowMonsterCtrl.OnHitting(_ctrlAvatar.FinalDamage());
            _monsterMiniWnd.SetHpRate(_nowMonsterCtrl._currentHPRate);
        }

        _infoStageWnd.SetUntilDamageCount(_nowFaliedCount, _limitFailedCount);
    }

    bool CheckUsedAllCard()
    {
        foreach(Card card in _ltCardInfos)
        {
            if (!card._isUsed && !card._isCoupleLockState && !card._isLockState)
                return false;
        }

        return true;
    }

    bool CheckMonsterAllDead(bool isCard = false)
    {   
        if(isCard && _nowMonsterCtrl._currentHPRate != 0)
            return (_nextMonsterIndex > _ltStageMonsterInfo.Count) ? true : false;
        else
            return (_nextMonsterIndex >= _ltStageMonsterInfo.Count) ? true : false;

        // return _nextMonsterIndex >= _ltStageMonsterInfo.Count;
    }

    IEnumerator RecreateMonster()
    {
        // 이전 몬스터 Die 애니메이션일때 미리 생성
        GameObject go = Instantiate(_prefabMonsters[(int)_ltStageMonsterInfo[_nextMonsterIndex]._shapeType], _spawnPointMonster.transform.position, _prefabMonsters[0].transform.rotation);
        MonsterControl mc = go.GetComponent<MonsterControl>();
        MonsterInfo mi = _ltStageMonsterInfo[_nextMonsterIndex];
        mc.InitInfoData(mi._name, mi._att, mi._def, mi._maxLife, mi._accuracy, mi._evasionRate, mi._rank, mi._charac);
        _nextMonsterIndex++;

        yield return new WaitForSeconds(5f);
        // 이전 몬스터 제거 및 생성된 몬스터 제어
        Destroy(_nowMonsterCtrl.gameObject);
        _nowMonsterCtrl = mc;

        _infoMessageBox.EnableInfoMessage(true, "Next Monster Will Appear");
        _monsterMiniWnd.InitInfoData(null, _nowMonsterCtrl._myName, _nowMonsterCtrl._currentHPRate,
                                    _nowMonsterCtrl._myCharacTxt, _nowMonsterCtrl._myRank);
        _nowMonsterCtrl.SetMovePosition(_spawnPointMonster.transform.GetChild(0).position);

        _infoStageWnd.SetPlayMonsterCount(_nextMonsterIndex, _limitMonsterCount);
        _nowFaliedCount = 0;
        _infoStageWnd.SetUntilDamageCount(_nowFaliedCount, _limitFailedCount);
    }

    /// <summary>
    /// GameState를 InitData로 바꿀시 설정하는 명령 함수.
    /// </summary>
    public void InitGameData()
    {
        _currentGameState = eTypeGameState.InitData;
        
        _infoMessageBox.EnableInfoMessage(true, "Wait a moment...");

        _timerWnd.SettingTime(_timeGamePlay);

        _infoStageWnd.InitSetting();

        _nowStageNumber = UserInfoManager._instance._nowStageNum;
        StageInfo info = ResourcePoolManager._instance.GetStageInfo(_nowStageNumber);
        // 최대 카드 생성 수 설정
        _cardCount = info._limitCardCount;
        // 최대 몬스터 생성 수 설정
        _limitMonsterCount = info._limitMonsterCount;
        // 각 몬스터 정보 설정
        for(int n = 0; n < info._ltMonsterIndexes.Count; n++)
        {
            MonsterInfo mon = ResourcePoolManager._instance.GetMonsterInfo(info._ltMonsterIndexes[n]);
            _ltStageMonsterInfo.Add(mon);
        }
    }

    /// <summary>
    /// GameState를 SettingScreenBG 바꿀시 설정하는 명령 함수.
    /// </summary>
    public void SettingGameBackground()
    {
        _currentGameState = eTypeGameState.SettingScreenBG;

        //ChangeBattleMap();

        // 아바타 스폰.
        GameObject go = Instantiate(_prefabAvatar, _spawnPointAvatar.transform.position, Quaternion.identity);
        _ctrlAvatar = go.GetComponent<AvatarControl>();

        // 몬스터 스폰
        go = Instantiate(_prefabMonsters[(int)_ltStageMonsterInfo[_nextMonsterIndex]._shapeType], _spawnPointMonster.transform.position, _prefabMonsters[0].transform.rotation);
        _nowMonsterCtrl = go.GetComponent<MonsterControl>();
        MonsterInfo mi = _ltStageMonsterInfo[_nextMonsterIndex];
        _nowMonsterCtrl.InitInfoData(mi._name, mi._att, mi._def, mi._maxLife, mi._accuracy, mi._evasionRate, mi._rank, mi._charac);
        _nextMonsterIndex++;

        GameAvatarAppearnce();
    }

    public void GameAvatarAppearnce()
    {
        _currentGameState = eTypeGameState.PlayerAppearance;

        // 플레이어를 이동시키도록 한다.
        _playerMiniWnd.InitInfoData(null, _ctrlAvatar._myName, _ctrlAvatar._currentHPRate);
        _ctrlAvatar.SetMovePosition(_spawnPointAvatar.transform.GetChild(0).position);
        _infoMessageBox.EnableInfoMessage(true, "Player Appearance");
    }

    public void GameMonsterAppearance()
    {
        _currentGameState = eTypeGameState.MonsterAppearance;

        _monsterMiniWnd.InitInfoData(null, _nowMonsterCtrl._myName, _nowMonsterCtrl._currentHPRate, 
                                    _nowMonsterCtrl._myCharacTxt, _nowMonsterCtrl._myRank);
        _nowMonsterCtrl.SetMovePosition(_spawnPointMonster.transform.GetChild(0).position);
        _infoMessageBox.EnableInfoMessage(true, "Monster Appearance");

        _infoStageWnd.SetPlayMonsterCount(_nextMonsterIndex, _limitMonsterCount);
    }

    public void GameCardCreate()
    {
        _currentGameState = eTypeGameState.CreateCard;
        _infoMessageBox.EnableInfoMessage(true, "Ready~~!");

        // StartCoroutine을 이용하여 카드를 화면에 뿌린다.
        StartCoroutine(CreateCardFromPosition());
    }

    public void GameStart()
    {
        _currentGameState = eTypeGameState.GameStart;

        _infoMessageBox.EnableInfoMessage(true, "Fight!!");
        _timeCheck = 0;

        _infoStageWnd.SetPlayMonsterCount(_nextMonsterIndex, _limitMonsterCount);
        _infoStageWnd.SetUntilDamageCount(_nowFaliedCount, _limitFailedCount);
    }

    public void GamePlay()
    {
        _currentGameState = eTypeGameState.GamePlay;

        _infoMessageBox.EnableInfoMessage();
    }

    public void RecreateCard()
    {
        _currentGameState = eTypeGameState.ReCreateCard;

        _infoMessageBox.EnableInfoMessage(true, "Reset Card!");

        for (int i = 0; i < _ltCardInfos.Count; i++)
        {
            Destroy(_ltCardInfos[i].gameObject);
        }
        _ltCardInfos.Clear();
        StartCoroutine(CreateCardFromPosition());
    }

    public void MonsterReApperance()
    {
        _currentGameState = eTypeGameState.MonsterReAppearance;

        _infoMessageBox.EnableInfoMessage(true, "Is it Over?");
        StartCoroutine(RecreateMonster());
    }

    public void LockCard()
    {
        _currentGameState = eTypeGameState.LockCard;

        int lockCardID = 0;
        int coupleCardID = 0;
        int checkAllCard = 0;
        do
        {
            lockCardID = Random.Range(0, _ltCardInfos.Count);
            checkAllCard++;
            if (checkAllCard > _ltCardInfos.Count)
                return;
        }
        while (_ltCardInfos[lockCardID]._isUsed || _ltCardInfos[lockCardID]._isFeverState || _ltCardInfos[lockCardID]._isLockState);

        _ltCardInfos[lockCardID].LockCard();

        for (int i = 0; i < _ltCardInfos.Count; i++)
        {
            if ((_ltCardInfos[i]._ICON_INDEX == _ltCardInfos[lockCardID]._ICON_INDEX) && (lockCardID != i))
            {
                coupleCardID = i;
                break;
            }
        }

        _ltCardInfos[coupleCardID]._isCoupleLockState = true;

        if (CheckUsedAllCard())
        {
            RecreateCard();
        }
    }

    public void GameEnd(bool isWin)
    {
        _currentGameState = eTypeGameState.EndGame;

        if(isWin)
            _infoMessageBox.EnableInfoMessage(true, "Game Clear!!");
        else
            _infoMessageBox.EnableInfoMessage(true, "Game End");

        _isSuccess = isWin;
        _timeCheck = 0;
    }

    public void GameResult()
    {
        _currentGameState = eTypeGameState.ResultGame;

        _infoMessageBox.EnableInfoMessage();

        if(_isSuccess)
        {
            StageInfo sI = ResourcePoolManager._instance.GetStageInfo(_nowStageNumber);
            int trophy = 0;
            if (sI._deathCountForTrophy >= 0)
                trophy++;
            if (_playCount <= sI._limitCardActionCountForTrophy)
                trophy++;
            if (_timeGamePlay <= sI._limitTimeForTrophy * 60)
                trophy++;

            UserInfoManager._instance.SetClearStageInfo(_nowStageNumber, trophy);
        }

        // 여기서 결과창을 생성
        ResultWindow resultWnd = Instantiate(_prefabResultWnd).GetComponent<ResultWindow>();
        resultWnd.OpenWindow(_isSuccess, _timeGamePlay, _nextMonsterIndex - 1, _limitMonsterCount);
    }
}
