using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl : BaseStatus
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _prefabEffHit;
    [SerializeField]
    Transform _Effectpos1;
#pragma warning restore

    Animator _aniControl;
    eTypeStateAction _currentState;

    Vector3 _goalPosition;

    private void Awake()
    {   
        _aniControl = GetComponent<Animator>();

        InitStatInfo("김철수", 5, 1, 100, 75.0f, 65.0f);
    }

    private void Start()
    {   
        //_aniControl.SetBool("IsRun", true);
    }

    private void Update()
    {
        if (_currentState == eTypeStateAction.RUN)
        {
            if (Vector3.Distance(transform.position, _goalPosition) < 0.1f)
            {   
                ChangeAction(eTypeStateAction.IDLE);
                transform.position = _goalPosition;
                // 아바타가 목표위치에 왔을 때 몬스터를 등장
                IngameManager._instance.GameMonsterAppearance();
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, _goalPosition, 5 * Time.deltaTime);
        }
    }

    private void ChangeAction(eTypeStateAction type)
    {
        switch(type)
        {
            case eTypeStateAction.IDLE:
                _aniControl.SetBool("IsRun", false);
                break;
            case eTypeStateAction.RUN:
                _aniControl.SetBool("IsRun", true);
                break;
            case eTypeStateAction.ATTACK:
                _aniControl.SetTrigger("Attack");
                break;
            case eTypeStateAction.HIT:
                _aniControl.SetTrigger("Hit");
                _aniControl.SetInteger("nowHP", _currentHP);
                break;
            case eTypeStateAction.DEAD:
                _aniControl.SetTrigger("Hit");
                _aniControl.SetInteger("nowHP", _currentHP);
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
        GameObject go = Instantiate(_prefabEffHit, _Effectpos1.position, Quaternion.identity);
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
            IngameManager._instance.GameEnd(false);
        }
    }

    public void Attack()
    {
        IngameManager._instance.Battle();
    }

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 150, 40), "IDLE"))
    //        //_aniControl.SetBool("IsRun", false);
    //        ChangeAction(eTypeStateAction.IDLE);

    //    if (GUI.Button(new Rect(155, 0, 150, 40), "RUN"))
    //        //_aniControl.SetBool("IsRun", true);
    //        ChangeAction(eTypeStateAction.RUN);

    //    if (GUI.Button(new Rect(0, 45, 150, 40), "ATTACK"))
    //        ChangeAction(eTypeStateAction.ATTACK);

    //    if (GUI.Button(new Rect(155, 45, 150, 40), "HIT"))
    //        ChangeAction(eTypeStateAction.HIT);
    //}
}
