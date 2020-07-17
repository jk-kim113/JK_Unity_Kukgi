using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    GameObject _prefabMonster;

    float _intervalCreateTime;
    float _timeCheck;

    private void Awake()
    {
        _prefabMonster = Resources.Load("Prefabs/Monster_Crawler") as GameObject;
        _intervalCreateTime = Random.Range(1.0f, 3.0f);
    }

    private void Update()
    {
        if (IngameManager._instance._nowTrackState != IngameManager.eTrackState.MonsterAppearance)
            return;

        _timeCheck += Time.deltaTime;

        if(_timeCheck > _intervalCreateTime)
        {
            _timeCheck = 0;
            _intervalCreateTime = Random.Range(5.0f, 8.0f);

            GameObject go = Instantiate(_prefabMonster, transform.position, transform.rotation);
        }
    }
}
