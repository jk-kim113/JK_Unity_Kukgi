using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int _phaseIndex = 0;
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

    public void InitSetting()
    {
        _playerSpawnPos = GameObject.Find("PlayerSpawnPoint").transform;
    }

    public Player SpawnPlayer()
    {
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
        if(CheckAllEndRewind())
            IngameManager._instance.SpawnPlayer();
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
