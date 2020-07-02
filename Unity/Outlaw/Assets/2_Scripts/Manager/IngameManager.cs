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
    bool _isFirstView = false;
#pragma warning restore

    public enum eStateFlower
    {
        none    = 0,
        Ready,
        SpawnPlayer,
        Start,
        Play,
        
        End,
        Reslut
    }

    List<SpawnControl> _spawnPointList = new List<SpawnControl>();
    Transform _playerSpawnPos;
    GameObject _prefabPlayer;
    GameObject _stickWindow;
    GameObject _miniStatusWindow;
    MessageBox _msgBox;
    

    Player _player;
    eStateFlower _currentState;
    float _timeCheck;
    public eStateFlower _nowGameState { get { return _currentState; } }

    public bool _firstView { get { return _isFirstView; } }

    //TimeBody _timeBody;

    GameObject _miniWnd;
    
    float _timePhase = 8.0f;

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;

        _prefabPlayer = Resources.Load("Prefabs/Characters/PlayerObject") as GameObject;
    }

    private void Start()
    {
        _msgBox = GameObject.FindGameObjectWithTag("MessageBox").GetComponent<MessageBox>();
        _stickWindow = GameObject.FindGameObjectWithTag("StickControllWindow");
        _miniStatusWindow = GameObject.FindGameObjectWithTag("MiniAvatarWindow");

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
                break;
            case eStateFlower.End:
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= 1)
                {
                    GameResult();
                }
                break;
        }

        //switch(_nowGameState)
        //{
        //    case eTypeGameState.GamePlay:
        //        _timePhase -= Time.deltaTime;
        //        if(_timePhase < 0.5)
        //        {
        //            Time.timeScale = 0.1f;
        //        }

        //        if(_timePhase < 0)
        //        {
        //            _timePhase = 0;
        //            Time.timeScale = 1.0f;
        //            _nowGameState = eTypeGameState.GameRewind;
        //        }

        //        _textTimePhase.text = string.Format("{0:F2}", _timePhase);
        //        break;

        //    case eTypeGameState.GameRewind:
        //        _timeBody.StartRewind();
        //        break;
        //}
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
        _playerSpawnPos = GameObject.Find("PlayerSpawnPoint").transform;
    }

    public void SpawnPlayer()
    {
        _currentState = eStateFlower.SpawnPlayer;

        _timeCheck = 0;
        _miniStatusWindow.SetActive(true);
        GameObject go = Instantiate(_prefabPlayer, _playerSpawnPos.position, _playerSpawnPos.rotation);
        _player = go.GetComponent<Player>();
        CameraController cc = Camera.main.GetComponent<CameraController>();
        cc.SetPlayer(go);

        // 임시
        GameStart();
    }

    public void GameStart()
    {
        _currentState = eStateFlower.Start;

        _msgBox.OpenMessageBox("플레이 시작~!~!", true);
        _timeCheck = 0;
    }

    void GamePlay()
    {
        _currentState = eStateFlower.Play;

        _stickWindow.SetActive(true);
        _msgBox.OpenMessageBox();
        _player.SettingSticks();
    }

    public void GameEnd(bool isWin)
    {
        _currentState = eStateFlower.End;

        _timeCheck = 0;
        if(isWin)
            _msgBox.OpenMessageBox("축하합니다~!~!", true);
        else
            _msgBox.OpenMessageBox("실력 부족!!", true);
    }

    public void GameResult()
    {
        _currentState = eStateFlower.Reslut;

        _msgBox.OpenMessageBox();
        // 모든 몬스터 삭제.
        // 결과창 열기.
        GameObject go = Resources.Load("Prefabs/UI/ResultWindowFrame") as GameObject;
        Instantiate(go);
    }
}
