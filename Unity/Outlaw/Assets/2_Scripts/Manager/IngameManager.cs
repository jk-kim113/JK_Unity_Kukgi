using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _textTimePhase;
    [SerializeField]
    Image _panelSlowEff;
    [SerializeField]
    GameObject _replayEffect;
    [SerializeField]
    bool _isFirstView = false;
#pragma warning restore

    public enum eStateFlower
    {
        none    = 0,
        Ready,
        SpawnPlayer,
        Start,
        Play,
        Rewind,
        End,
        Reslut
    }

    List<SpawnControl> _spawnPointList = new List<SpawnControl>();

    GameObject _prefabResultWnd;
    GameObject _stickWindow;
    GameObject _miniStatusWindow;
    MessageBox _msgBox;
    ResultWindow resultWnd;

    Transform _canvasTr;

    Player _player;
    eStateFlower _currentState;
    float _timeCheck;
    public eStateFlower _nowGameState { get { return _currentState; } }

    public bool _firstView { get { return _isFirstView; } }

    GameObject _miniWnd;
    
    float _timePhase = 13.0f;

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;

        _prefabResultWnd = Resources.Load("Prefabs/UI/ResultWindowFrame") as GameObject;
    }

    private void Start()
    {
        _msgBox = GameObject.FindGameObjectWithTag("MessageBox").GetComponent<MessageBox>();
        _stickWindow = GameObject.FindGameObjectWithTag("StickControllWindow");
        _miniStatusWindow = GameObject.FindGameObjectWithTag("MiniAvatarWindow");
        _canvasTr = GameObject.Find("IngameUIFrame").transform;

        _msgBox.OpenMessageBox();
        _stickWindow.SetActive(false);
        _miniStatusWindow.SetActive(false);

        _textTimePhase.text = string.Format("{0:F2}", _timePhase);
    }

    private void Update()
    {
        switch(_currentState)
        {
            case eStateFlower.none:
                if (SceneControlManager._instance._nowLoadingSate == SceneControlManager.eLoadingState.LoadEnd)
                    GameReady();
                break;
            case eStateFlower.Ready:
                _timeCheck += Time.deltaTime;
                if(_timeCheck >= 3)
                {
                    SpawnPlayer();
                }
                break;
            case eStateFlower.Start:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 1)
                {
                    GamePlay();
                }
                break;
            case eStateFlower.Play:
                //GameEnd(CheckClearConditions());
                if (CheckClearConditions())
                {
                    GameEnd(true);
                }
                if(_player._isDead)
                {
                    GameEnd(false);
                }

                _timePhase -= Time.deltaTime;
                if (_timePhase < 0.5)
                {
                    _panelSlowEff.gameObject.SetActive(true);

                    if (PlayerManager._instance._nowPhase != 3)
                    {
                        _panelSlowEff.color = new Color(_panelSlowEff.color.r, _panelSlowEff.color.g, _panelSlowEff.color.b,
                                                    _panelSlowEff.color.a + Time.deltaTime * 1.3f);
                    }
                        
                    Time.timeScale = 0.2f;
                }

                if (_timePhase < 0)
                {
                    if (PlayerManager._instance._nowPhase != 3)
                    {
                        GameObject go = Instantiate(_replayEffect, _canvasTr);
                        Destroy(go, 1f);
                    }   

                    _timePhase = 0;
                    _panelSlowEff.color = new Color(_panelSlowEff.color.r, _panelSlowEff.color.g, _panelSlowEff.color.b, 0);
                    _panelSlowEff.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                    if(PlayerManager._instance._nowPhase == 3)
                    {
                        if (CheckClearConditions())
                        {
                            GameEnd(true);
                        }
                        else
                        {
                            GameEnd(false);
                        }
                    }
                    else
                    {
                        GameRewind();
                    }
                }

                _textTimePhase.text = string.Format("{0:F2}", _timePhase);

                break;
            case eStateFlower.Rewind:
                Time.timeScale = 3.5f;
                _timePhase += Time.deltaTime;

                if (_timePhase > 13)
                {
                    _timePhase = 13;
                    Time.timeScale = 1.0f;
                }

                _textTimePhase.text = string.Format("{0:F2}", _timePhase);
                break;
            case eStateFlower.End:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 1)
                {
                    GameResult();
                }
                break;
        }
    }

    void ListUpSpawnControl()
    {
        SpawnControl[] scArr = GameObject.FindObjectsOfType<SpawnControl>();
        for (int n = 0; n < scArr.Length; n++)
        {
            _spawnPointList.Add(scArr[n]);
        }
    }

    bool CheckClearConditions()
    {
        int cnt = 0;
        foreach(SpawnControl item in _spawnPointList)
        {
            if (item._checkRemainingCount)
                cnt++;
        }

        if (cnt >= 3)
            return true;
        else
            return false;
    }

    public void ReceivePlayerDie()
    {
        for (int n = 0; n < _spawnPointList.Count; n++)
            _spawnPointList[n].AllNotificationPlayerDeath();
    }

    public void GameReady()
    {
        _currentState = eStateFlower.Ready;

        _msgBox.OpenMessageBox("준비~!~!", true);
        ListUpSpawnControl();
        PlayerManager._instance.InitSetting();
    }

    public void SpawnPlayer()
    {
        _currentState = eStateFlower.SpawnPlayer;

        _timeCheck = 0;
        _miniStatusWindow.SetActive(true);

        _player = PlayerManager._instance.SpawnPlayer();

        // 임시
        GameStart();
    }

    public void GameStart()
    {
        _currentState = eStateFlower.Start;
        _timePhase = 13.0f;
        _msgBox.OpenMessageBox(PlayerManager._instance._nowPhase + " Pgase 시작~!~!", true);
        _timeCheck = 0;
    }

    void GamePlay()
    {
        _currentState = eStateFlower.Play;

        _stickWindow.SetActive(true);
        _msgBox.OpenMessageBox();
        _player.SettingSticks();
    }

    void GameRewind()
    {
        _currentState = eStateFlower.Rewind;

        for (int n = 0; n < _spawnPointList.Count; n++)
            _spawnPointList[n].ResetCount();

        PlayerManager._instance.StartRewind();
    }

    public void GameEnd(bool isWin)
    {
        _currentState = eStateFlower.End;

        PlayerManager._instance.ResetPlayer();

        _timeCheck = 0;
        if(isWin)
            _msgBox.OpenMessageBox("축하합니다~!~!", true);
        else
            _msgBox.OpenMessageBox("실력 부족!!", true);

        for (int n = 0; n < _spawnPointList.Count; n++)
            _spawnPointList[n].ResetCount();

        GameObject go = Instantiate(_prefabResultWnd);
        resultWnd = go.GetComponent<ResultWindow>();
        resultWnd.OpenWindow(isWin);
        resultWnd.gameObject.SetActive(false);
    }

    public void GameResult()
    {
        _currentState = eStateFlower.Reslut;

        _msgBox.OpenMessageBox();
        // 모든 몬스터 삭제.
        // 결과창 열기.

        resultWnd.gameObject.SetActive(true);
    }
}
