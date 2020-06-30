using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : UnitBase
{
    enum eAniKeyType
    {
        IDLE         = 0,
        WALK,
        RUN,
        ATTACK,
        HIT,
        DEAD
    }

    public enum eTypeRoam
    {
        Random      = 0,
        Loop,
        PingPong,

        max
    }

    public enum eKindRoam
    {
        Random      = 0,
        Patrol,
        Border,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    float _minWaitTime = 3.5f;
    [SerializeField]
    float _maxWaitTime = 9.9f;
    [SerializeField]
    HitZone _hitZone;
    [SerializeField]
    SightZone _sightZone;
    [SerializeField]
    WorldStatusWindow _statusMonWnd;
#pragma warning restore

    GameObject _prefabEffHitBlood;

    eTypeRoam _roamingType = eTypeRoam.Random;
    eKindRoam _roamingKind = eKindRoam.Random;
    eAniType _nowAction;
    public eAniType _curAction { get { return _nowAction; } }
    Animation _ctrlAni;
    NavMeshAgent _navAgent;
    Dictionary<eAniKeyType, string> _aniList = new Dictionary<eAniKeyType, string>();
    List<Vector3> _roamPointList = new List<Vector3>();
    Player _targetPlayer;
    SpawnControl _ownerParents;

    // Monster의 기본 정보.
    float _runSpeed = 4;
    float _walkSpeed = 2.7f;
    float _rateMoveAct = 50.0f;
    float _timeWait = 0;
    float _sightRange = 10f;
    float _attackRange = 3f;
    float _followDistance = 20.0f;

    // Monster의 활용 정보.
    Vector3 _posGoal;
    float _movSpeed;
    int _nowIndex = -1;
    bool _isBack = false;
    bool _isSelectAct = false;
    bool _isBorder = false;
    int _randomRoamCnt = 0;
    Vector3 _posBattleStart;

    public float _lengthSight { get { return _sightRange; } }
    public int _finalDamage { get { return _baseAtt; } }

    private void Awake()
    {
        _prefabEffHitBlood = Resources.Load("Prefabs/ParticleEffects/HitBlood") as GameObject;

        _ctrlAni = GetComponent<Animation>();
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.enabled = false;
        MyAnimationList();

        InitializeData("rrr", 20, 3, 1);
    }

    private void Start()
    {
        _hitZone.InitSetting(this);
        _sightZone.InitSetting(this);
        _ctrlAni.Play(_aniList[eAniKeyType.IDLE]);
        _hitZone.EnableTrigger(false);
    }

    private void Update()
    {
        if (_isDead || IngameManager._instance._nowGameState != IngameManager.eStateFlower.Play)
            return;

        if (!_ctrlAni.isPlaying)
            ChangedAction(eAniType.IDLE);

        switch (_nowAction)
        {
            case eAniType.WALK:
                if (Vector3.Distance(transform.position, _navAgent.destination) < 0.1f)
                    _isSelectAct = false;
                break;
            case eAniType.IDLE:
                _timeWait -= Time.deltaTime;
                if(_timeWait <= 0)
                    _isSelectAct = false;
                break;
            case eAniType.RUN:
                if (Vector3.Distance(transform.position, _posBattleStart) >= _followDistance)
                {
                    ChangedAction(eAniType.BACKHOME);
                    _navAgent.destination = _posBattleStart;
                }
                else if (Vector3.Distance(transform.position, _navAgent.destination) < (_attackRange - 0.5f))
                    ChangedAction(eAniType.ATTACK);
                else
                    _navAgent.destination = _targetPlayer.transform.position;
                break;
            case eAniType.ATTACK:
                if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > _attackRange)
                {
                    ChangedAction(eAniType.RUN);
                    _navAgent.destination = _targetPlayer.transform.position;
                }
                else
                {
                    AnimationState anistate = _ctrlAni[_aniList[eAniKeyType.ATTACK]];
                    if((anistate.normalizedTime * 100) % 100 > 33)
                    {
                        _hitZone.EnableTrigger(true);
                    }
                    if((anistate.normalizedTime * 100) % 100 > 60)
                    {
                        _hitZone.EnableTrigger(false);
                    }
                }
                break;
            case eAniType.BACKHOME:
                if (Vector3.Distance(transform.position, _posBattleStart) < 0.1f)
                {
                    transform.position = _posBattleStart;
                    _isSelectAct = false;
                    _targetPlayer = null;
                }
                break;
        }

        // AI 선택에 대한 프로세스
        SelectAIProcess();
    }

    public void SettingGoalPosition(Vector3 point, bool isRun = false)
    {
        if (isRun)
            ChangedAction(eAniType.RUN);
        else
            ChangedAction(eAniType.WALK);

        _navAgent.destination = point;
    }

    public void SetRoamPositions(Transform root, eTypeRoam type, eKindRoam kind, SpawnControl owner)
    {
        for(int n = 0; n < root.childCount; n++)
        {
            _roamPointList.Add(root.GetChild(n).position);
        }

        _roamingType = type;
        _roamingKind = kind;
        _ownerParents = owner;
    }
    
    public void OnBattle(Player p)
    {
        if (_targetPlayer != null)
            return;

        _posBattleStart = transform.position;
        _targetPlayer = p;
        // 전투시에 일어나야 할 설정.
        if(Vector3.Distance(transform.position, _targetPlayer.transform.position) <= _attackRange)
            ChangedAction(eAniType.ATTACK);
        else
        {
            ChangedAction(eAniType.RUN);
            _navAgent.destination = _targetPlayer.transform.position;
        }

        _ownerParents.AttackAtOnce(p);
    }

    public void Winner()
    {
        if (_targetPlayer == null)
            return;

        StartCoroutine(WinnerAction());
    }

    IEnumerator WinnerAction()
    {
        ChangedAction(eAniType.IDLE);
        yield return new WaitForSeconds(2);

        ChangedAction(eAniType.BACKHOME);
        _navAgent.destination = _posBattleStart;

        yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();
    }

    /// <summary>
    /// 매 프레임 AI가 선택을 할 수 있는지 확인하고, 선택할 수 있다면 행동에 대한
    /// 선택(대기, 이동)을 하도록 한다.
    /// </summary>
    void SelectAIProcess()
    {
        if (!_isSelectAct)// 선택이 되어 있지 않을 때만 선택을 하도록 한다.
        {
            switch (_roamingKind)
            {
                case eKindRoam.Random:
                    float rate = Random.Range(0.0f, 100.0f);
                    if (rate <= _rateMoveAct)
                    {
                        SettingGoalPosition(GetNextPosition());
                    }
                    else
                    {
                        _timeWait = Random.Range(_minWaitTime, _maxWaitTime);
                        ChangedAction(eAniType.IDLE);
                    }
                    break;

                case eKindRoam.Patrol:
                    SettingGoalPosition(GetNextPosition());
                    break;

                case eKindRoam.Border:
                    if (!_isBorder)
                    {
                        SettingGoalPosition(GetNextPosition());
                    }
                    else
                    {
                        _timeWait = Random.Range(_minWaitTime, _maxWaitTime);
                        ChangedAction(eAniType.IDLE);
                        _isBorder = false;
                    }
                    break;
            }

            _isSelectAct = true;
        }
    }

    Vector3 GetNextPosition()
    {
        switch(_roamingType)
        {
            case eTypeRoam.Random:
                _nowIndex = Random.Range(0, _roamPointList.Count);
                _randomRoamCnt++;
                if(_randomRoamCnt >= _roamPointList.Count)
                {
                    _isBorder = true;
                    _randomRoamCnt = 0;
                }
                break;

            case eTypeRoam.Loop:
                _nowIndex++;
                if (_nowIndex >= _roamPointList.Count)
                {
                    _isBorder = true;
                    _nowIndex = 0;
                }
                break;

            case eTypeRoam.PingPong:

                if (_isBack)
                {
                    _nowIndex--;
                    if(_nowIndex == 0)
                        _isBorder = true;

                    if (_nowIndex < 0)
                    {
                        _nowIndex = 1;
                        _isBack = false;
                    }
                }   
                else
                {
                    _nowIndex++;
                    if(_nowIndex == _roamPointList.Count - 1)
                        _isBorder = true;

                    if (_nowIndex >= _roamPointList.Count)
                    {
                        _nowIndex = _roamPointList.Count - 2;
                        _isBack = true;
                    }
                }

                break;
        }

        return _roamPointList[_nowIndex];
    }

    void MyAnimationList()
    {
        int cnt = 0;
        foreach(AnimationState state in _ctrlAni)
        {
            _aniList.Add((eAniKeyType)cnt, state.name);
            cnt++;
        }
    }

    void ChangedAction(eAniType type)
    {
        switch(type)
        {
            case eAniType.IDLE:
                _navAgent.enabled = false;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.IDLE]);
                break;
            case eAniType.WALK:
                _navAgent.enabled = true;
                _navAgent.speed = _walkSpeed;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.WALK]);
                break;
            case eAniType.RUN:
                _navAgent.enabled = true;
                _navAgent.speed = _runSpeed;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.RUN]);
                break;
            case eAniType.ATTACK:
                _navAgent.enabled = false;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.ATTACK]);
                break;
            case eAniType.BACKHOME:
                _navAgent.enabled = true;
                _navAgent.speed = _runSpeed * 2;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.RUN]);
                break;
            case eAniType.DEAD:
                _navAgent.enabled = false;
                _ctrlAni.CrossFade(_aniList[eAniKeyType.DEAD]);
                _isDead = true;
                break;
        }

        _nowAction = type;
    }

    public bool OnHitting(int hitDamage)
    {
        if (!_isDead)
        {
            if (HittingMe(hitDamage))
            { // 죽었을 때
                ChangedAction(eAniType.DEAD);
                GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject, 3f);
            }
            else
            { // 살았을 때

            }

            _statusMonWnd.SettingHPBar(_hpRate);
        }

        return _isDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletObj"))
        {
            //Quaternion q = Quaternion.Euler(other.transform.eulerAngles + new Vector3(0, 180, 0));
            GameObject go = Instantiate(_prefabEffHitBlood, other.transform.position,
                                        Quaternion.LookRotation(other.transform.position - transform.position));

            Bullet bullet = other.GetComponent<Bullet>();

            if(OnHitting(bullet._finalDamage))
            {

            }
            else
            {
                OnBattle((Player)bullet._ownerTarget);
            }

            Destroy(other.gameObject);
            Destroy(go, 3.0f);
        }
    }

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 300, 100), "IDLE"))
    //    {
    //        ChangedAction(eAniType.IDLE);
    //    }
    //    else if (GUI.Button(new Rect(300, 0, 300, 100), "WALK"))
    //    {
    //        ChangedAction(eAniType.WALK);
    //    }
    //    else if (GUI.Button(new Rect(600, 0, 300, 100), "ATTACK"))
    //    {
    //        ChangedAction(eAniType.ATTACK);
    //    }
    //}
}
