﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    bool _isRandom = false;
    [SerializeField]
    Monster.eTypeRoam _typeRoam = Monster.eTypeRoam.Random;
    [SerializeField]
    Monster.eKindRoam _kindRoam = Monster.eKindRoam.Random;
    [SerializeField]
    int _maxViewCount = 3;
    [SerializeField]
    int _maxCreateCount = 10;
    [SerializeField]
    float _intervalCreateTime = 2;
#pragma warning restore

    GameObject _prefabMon;
    float _timeCheck = 0;
    List<GameObject> _spawnMonList = new List<GameObject>();

    private void Awake()
    {
        _prefabMon = Resources.Load("Prefabs/Characters/MonGhost") as GameObject;
    }

    private void Update()
    {
        if (_maxCreateCount > 0)
        {
            if(_spawnMonList.Count < _maxViewCount)
            {
                _timeCheck += Time.deltaTime;
                if (_timeCheck >= _intervalCreateTime)
                {
                    _timeCheck = 0;
                    GameObject go = Instantiate(_prefabMon, transform.position, transform.rotation);
                    Monster mon = go.GetComponent<Monster>();
                    mon.InitSetting(this);
                    if(_isRandom)
                    {
                        int type, kind;
                        type = 0;
                        kind = 0;
                        type += 0;
                        kind += 0;
                        // _typeRoam 랜덤 선택.
                        // _kindRoam 랜덤 선택.
                    }
                    else
                    {
                        mon.SetRoamPositions(transform.GetChild(0), _typeRoam, _kindRoam);
                    }
                    
                    _spawnMonList.Add(go);
                    _maxCreateCount--;
                }
            }
        }
    }

    private void LateUpdate()
    {
        for(int n = 0; n < _spawnMonList.Count; n++)
        {
            if(_spawnMonList[n] == null)
            {
                _spawnMonList.RemoveAt(n);
                break;
            }
        }
    }

    public void GroupBattle(Player target)
    {
        for(int n = 0; n < _spawnMonList.Count; n++)
        {
            _spawnMonList[n].GetComponent<Monster>().OnBattle(target);
        }
    }
}
