using System.Collections;
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
    int _nowCreateCount;

    public bool _checkRemainingCount { get { return (_maxCreateCount == 0 &&_spawnMonList.Count == 0); } }

    private void Awake()
    {
        _prefabMon = Resources.Load("Prefabs/Characters/MonGhost") as GameObject;

        _nowCreateCount = _maxCreateCount;
    }

    public void ResetCount()
    {
        for (int n = 0; n < _spawnMonList.Count; n++)
            Destroy(_spawnMonList[n].gameObject);
        _spawnMonList.Clear();

        _maxCreateCount = _nowCreateCount;
    }

    private void Update()
    {
        if (IngameManager._instance._nowGameState != IngameManager.eStateFlower.Play)
            return;

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
                    if(_isRandom)
                    {
                        int type, kind;
                        type = Random.Range(0, (int)Monster.eTypeRoam.max);
                        kind = Random.Range(0, (int)Monster.eKindRoam.max); ;

                        mon.SetRoamPositions(transform.GetChild(0), (Monster.eTypeRoam)type, (Monster.eKindRoam)kind, this);
                    }
                    else
                    {
                        mon.SetRoamPositions(transform.GetChild(0), _typeRoam, _kindRoam, this);
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

    /// <summary>
    /// 이 컨트롤러에 속한 몬스터들의 전체 공격
    /// </summary>
    /// <param name="p">공격 목표</param>
    public void AttackAtOnce(Player p)
    {
        for (int n = 0; n < _spawnMonList.Count; n++)
        {
            Monster m = _spawnMonList[n].GetComponent<Monster>();
            m.OnBattle(p);
        }
    }

    /// <summary>
    /// 이 컨트롤러에 속한 몬스터들에게 타깃이던 플레이어가 죽었음을 알린다.
    /// </summary>
    public void AllNotificationPlayerDeath()
    {
        for (int n = 0; n < _spawnMonList.Count; n++)
        {
            Monster m = _spawnMonList[n].GetComponent<Monster>();
            m.Winner();
        }
    }
}
