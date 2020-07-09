using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int _phaseIndex = 0;
    public int _nowPhase { get { return _phaseIndex; } }
    List<Player> _spawnedPlayer = new List<Player>();
    GameObject _prefabPlayer;
    Transform _playerSpawnPos;

    static PlayerManager _uniqueInstance;
    public static PlayerManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
        _prefabPlayer = Resources.Load("Prefabs/Characters/PlayerObject") as GameObject;
    }

    private void FixedUpdate()
    {
        if (IngameManager._instance._nowGameState != IngameManager.eStateFlower.Play || _phaseIndex == 1)
            return;

        CheckList();
    }

    public void InitSetting()
    {
        _playerSpawnPos = GameObject.Find("PlayerSpawnPoint").transform;
    }

    public Player SpawnPlayer()
    {
        SetListSameCount();

        GameObject go = Instantiate(_prefabPlayer, _playerSpawnPos.position, _playerSpawnPos.rotation);
        Player player = go.GetComponent<Player>();
        _spawnedPlayer.Add(player);
        _phaseIndex++;
        CameraController cc = Camera.main.GetComponent<CameraController>();
        cc.SetPlayer(go);

        return player;
    }

    public void StartRewind()
    {
        for (int n = 0; n < _spawnedPlayer.Count; n++)
        {
            _spawnedPlayer[n].StartRewind();
        }
    }

    public void EndRewind()
    {
        if (CheckAllEndRewind())
            IngameManager._instance.SpawnPlayer();
    }

    public void ResetPlayer()
    {
        // Stop Anim 
    }

    void SetListSameCount()
    {
        if (_phaseIndex <= 1)
            return;

        int firstCount = _spawnedPlayer[_phaseIndex - 2]._moveListCount;
        int secondCount = _spawnedPlayer[_phaseIndex - 1]._moveListCount;

        Debug.Log("first count : " + firstCount + " / second count : " + secondCount);

        if (firstCount == secondCount)
            return;
        else if (firstCount > secondCount)
        {
            int delta = firstCount - secondCount;
            int term = secondCount / delta;

            for (int n = term; n < secondCount; n = n + term)
                _spawnedPlayer[_phaseIndex - 1].ChangeMoveList(n, 1, true);
        }
        else if (secondCount > firstCount)
        {
            int delta = secondCount - firstCount;
            int term = firstCount / delta;

            for (int n = 0; n < _phaseIndex - 2; n++)
                for (int m = term; m < firstCount; m = m + term)
                    _spawnedPlayer[n].ChangeMoveList(m, 1, true);
        }

        for (int n = 0; n < _spawnedPlayer.Count; n++)
            Debug.Log(n + " : " + _spawnedPlayer[n]._moveListCount);
    }

    void CheckList()
    {
        int firstID = _spawnedPlayer[_phaseIndex - 2]._movePointID;
        int currentListCount = _spawnedPlayer[_phaseIndex - 1]._moveListCount;

        if (firstID == currentListCount)
            return;

        for (int n = 0; n < _phaseIndex - 2; n++)
            _spawnedPlayer[n].ChangeMoveList(firstID, Mathf.Abs(currentListCount - firstID), firstID < currentListCount);
    }

    bool CheckAllEndRewind()
    {
        for(int n = 0; n < _spawnedPlayer.Count; n++)
        {
            if (_spawnedPlayer[n].isRewind)
                return false;
        }

        return true;
    }
}
