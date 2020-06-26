using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : BaseStatus
{
    public enum eTypeEffect
    {
        Hit     = 0,
        Miss
    }

#pragma warning disable 0649
    [SerializeField]
    GameObject[] _prefabEffHit;
    [SerializeField]
    Transform _Effectpos1;
#pragma warning restore

    // 고유 스탯
    eTypeRank _rank;
    eTypeCharacter _charac;
    string _strCharac;
    //================

    Animator _ctrlAni;
    eTypeStateAction _currentState;
    Vector3 _goalPosition;
    Transform _parentTr;

    public string _myCharacTxt { get { return _strCharac; } }

    public eTypeRank _myRank { get { return _rank; } }

    private void Awake()
    {
        _ctrlAni = GetComponent<Animator>();
    }

    private void Start()
    {
        _parentTr = GameObject.Find("IngameUIFrame").transform;
    }

    private void Update()
    {
        if (_currentState == eTypeStateAction.RUN)
        {
            if (Vector3.Distance(transform.position, _goalPosition) < 0.1f)
            {
                ChangeAction(eTypeStateAction.IDLE);
                transform.position = _goalPosition;
                if (IngameManager._instance._nowGameState == IngameManager.eTypeGameState.MonsterAppearance)
                    IngameManager._instance.GameCardCreate();
                else
                    IngameManager._instance.GamePlay();
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, _goalPosition, 5 * Time.deltaTime);
        }
    }

    public void InitInfoData(string name, int att, int def, int hp, float acc, float evas, eTypeRank rank, eTypeCharacter ch)
    {
        InitStatInfo(name, att, def, hp, acc, evas);
        _rank = rank;
        _charac = ch;
        _strCharac = GetStringToEnumCharacter(ch);
    }

    string GetStringToEnumCharacter(eTypeCharacter ch)
    {
        string re = string.Empty;
        switch (ch)
        {
            case eTypeCharacter.Lazy:
                re = "게으른";
                break;
            case eTypeCharacter.Fool:
                re = "바보 같은";
                break;
            case eTypeCharacter.Normal:
                re = "평범한";
                break;
            case eTypeCharacter.Impatient:
                re = "예민한";
                break;
            case eTypeCharacter.Ferocious:
                re = "난폭한";
                break;
        }

        return re;
    }

    private void ChangeAction(eTypeStateAction type)
    {
        switch (type)
        {
            case eTypeStateAction.IDLE:
                _ctrlAni.SetBool("IsRun", false);
                break;
            case eTypeStateAction.RUN:
                _ctrlAni.SetBool("IsRun", true);
                break;
            case eTypeStateAction.ATTACK:
                _ctrlAni.SetTrigger("Attack");
                break;
            case eTypeStateAction.HIT:
                _ctrlAni.SetTrigger("Hit");
                _ctrlAni.SetInteger("nowHP", _currentHP);
                break;
            case eTypeStateAction.DEAD:
                _ctrlAni.SetTrigger("Hit");
                _ctrlAni.SetInteger("nowHP", _currentHP);
                break;
        }

        _currentState = type;
    }

    public void SetMovePosition(Vector3 point)
    {
        _goalPosition = point;
        ChangeAction(eTypeStateAction.RUN);
    }

    public void AttackAction()
    {
        ChangeAction(eTypeStateAction.ATTACK);
    }

    public override int FinalDamage()
    {
        return _attackPow;
    }
    public override int FinishDeffence()
    {
        return _deffencePow;
    }

    public override void OnHitting(int Damage)
    {
        float evasion = Random.Range(0, 100f);
        if(evasion < _evasionPow)
        {
            GameObject go = Instantiate(_prefabEffHit[(int)eTypeEffect.Miss], _parentTr);
            go.transform.position = Camera.main.WorldToScreenPoint(_Effectpos1.position);
            Destroy(go, 2f);

            IngameManager._instance.LockCard();
        }
        else
        {
            // Effect 생성
            GameObject go = Instantiate(_prefabEffHit[(int)eTypeEffect.Hit], _Effectpos1.position, Quaternion.identity);
            Destroy(go, 2f);

            ChangeAction(eTypeStateAction.HIT);
            int finishDamage = Damage - _deffencePow;
            if (finishDamage < 1)
                finishDamage = 1;
            _currentHP -= finishDamage;
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                ChangeAction(eTypeStateAction.DEAD);
                IngameManager._instance.MonsterPostTreatment();
            }
        }
    }

    public void Attack()
    {
        IngameManager._instance.Battle(true);
    }
}
