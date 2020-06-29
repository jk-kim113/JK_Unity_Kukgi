using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _textTimePhase;
#pragma warning restore

    public enum eTypeGameState
    {
        InitGame    = 0,
        Ready, 
        PlayerSpawn,
        GameStart,
        GamePlay,
        GameRewind,
        GameEnd,
        ShowReslut
    }

    List<SpawnControl> _spawnPointList = new List<SpawnControl>();

    eTypeGameState _nowGameState;
    public eTypeGameState _curGameState { get { return _nowGameState; } }

    GameObject _prefabPlayer;
    TimeBody _timeBody;

    GameObject _miniWnd;

    InfoMessage _infoWnd;

    float _timePhase = 8.0f;

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _textTimePhase.text = string.Format("{0:F2}", _timePhase);
        InitGameData();
    }

    private void Update()
    {
        switch(_nowGameState)
        {
            case eTypeGameState.GamePlay:
                _timePhase -= Time.deltaTime;
                if(_timePhase < 0.5)
                {
                    Time.timeScale = 0.1f;
                }

                if(_timePhase < 0)
                {
                    _timePhase = 0;
                    Time.timeScale = 1.0f;
                    _nowGameState = eTypeGameState.GameRewind;
                }

                _textTimePhase.text = string.Format("{0:F2}", _timePhase);
                break;

            case eTypeGameState.GameRewind:
                _timeBody.StartRewind();
                break;
        }
    }

    void InitGameData()
    {
        _nowGameState = eTypeGameState.InitGame;

        _miniWnd = GameObject.Find("MiniAvatarWindow");
        _miniWnd.SetActive(false);

        _infoWnd = GameObject.Find("InfoMessageWindow").GetComponent<InfoMessage>();
        _infoWnd.gameObject.SetActive(false);

        ListUpSpawnControl();
        _prefabPlayer = Resources.Load("Prefabs/Characters/PlayerObject") as GameObject;
        _timeBody = _prefabPlayer.GetComponent<TimeBody>();

        ReadyGame();
    }

    void ReadyGame()
    {
        _nowGameState = eTypeGameState.Ready;

        StartCoroutine(GameReadyCoroutine());
    }

    IEnumerator GameReadyCoroutine()
    {
        _infoWnd.EnableInfoMessage(true, "Ready");

        yield return new WaitForSeconds(1.5f);

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        _nowGameState = eTypeGameState.PlayerSpawn;

        _miniWnd.SetActive(true);

        Vector3 sapwnPos = GameObject.Find("PlayerSpawnPoint").transform.position;
        Instantiate(_prefabPlayer, sapwnPos, _prefabPlayer.transform.rotation);

        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        _infoWnd.EnableInfoMessage(true, "Game Start");
        yield return new WaitForSeconds(1f);

        for (int n = 3; n > 0 ; n--)
        {   
            _infoWnd.EnableInfoMessage(true, n.ToString());
            yield return new WaitForSeconds(1f);
        }

        _infoWnd.EnableInfoMessage(false);

        GameStart();

        StopAllCoroutines();
    }

    void GameStart()
    {
        _nowGameState = eTypeGameState.GameStart;

        GamePlay();
    }

    void GamePlay()
    {
        _nowGameState = eTypeGameState.GamePlay;
    }

    public void GameEnd()
    {
        _nowGameState = eTypeGameState.GameEnd;

        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        _infoWnd.EnableInfoMessage(true, "Game Clear");

        yield return new WaitForSeconds(2f);

        _infoWnd.EnableInfoMessage(false);

        GameResult();

        StopAllCoroutines();
    }

    void GameResult()
    {
        _nowGameState = eTypeGameState.ShowReslut;

        GameObject go = Resources.Load("Prefabs/UI/ResultWindowFrame") as GameObject;
        Instantiate(go);
    }

    void ListUpSpawnControl()
    {
        SpawnControl[] scArr = GameObject.FindObjectsOfType<SpawnControl>();
        for(int n = 0; n < scArr.Length; n++)
        {
            _spawnPointList.Add(scArr[n]);
        }
    }

    public void ReceivePlayerDie()
    {
        for (int n = 0; n < _spawnPointList.Count; n++)
            _spawnPointList[n].AllNotificationPlayerDeath();
    }

    public void CheckAllSpawnPoint()
    {
        for (int n = 0; n < _spawnPointList.Count; n++)
        {
            if (!_spawnPointList[n].CheckAllMonsterDead())
                return;
        }

        GameEnd();
    }
}
