using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public enum eTypeGameState
    {
        InitGame    = 0,
        Ready, 
        PlayerSpawn,
        GameStart,
        GamePlay,
        GameEnd,
        ShowReslut
    }

    List<SpawnControl> _spawnPointList = new List<SpawnControl>();

    eTypeGameState _nowGameState;
    public eTypeGameState _curGameState { get { return _nowGameState; } }

    GameObject _prefabPlayer;
    GameObject _miniWnd;

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        InitGameData();
    }

    void InitGameData()
    {
        _nowGameState = eTypeGameState.InitGame;

        _miniWnd = GameObject.Find("MiniAvatarWindow");
        _miniWnd.SetActive(false);

        ListUpSpawnControl();
        _prefabPlayer = Resources.Load("Prefabs/Characters/PlayerObject") as GameObject;

        ReadyGame();
    }

    void ReadyGame()
    {
        _nowGameState = eTypeGameState.Ready;

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        _nowGameState = eTypeGameState.PlayerSpawn;

        _miniWnd.SetActive(true);

        Vector3 sapwnPos = GameObject.Find("PlayerSpawnPoint").transform.position;
        Instantiate(_prefabPlayer, sapwnPos, _prefabPlayer.transform.rotation);

        GameStart();
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

        GameResult();
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
