﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
#pragma warning disable 0649
    [SerializeField]
    Transform _posFire;
    [SerializeField]
    Transform _posLook;
#pragma warning restore

    CharacterController _controller;
    Animator _animControl;
    GameObject _modelObj;
    GameObject _prefabbullet;

    // UI Reference
    StickObject _stickLauncher;
    StickObject _stickMovement;
    MiniStatusWindow _wndPlayerStatus;

    eAniType _nowAction;
    public int _curAction
    {
        get { return (int)_nowAction; }
        set
        {
            _nowAction = (eAniType)value;
            ChangedAction(_nowAction);
        }
    }

    // Player 기본 정보
    float _runSpeed = 5;
    float _walkSpeed = 1f;
    int _limitBulletCount = 24;

    // Player 활용 정보
    float _movSpeed;
    //bool _isAttack = false;
    int _curBulletCount = 0;

    bool isRewind = false;
    public bool _isRewinding { set { isRewind = value; } }

    public int _finalDamage { get { return _baseAtt; } }

    public float _bulletRate { get { return (float)(_limitBulletCount - _curBulletCount) / _limitBulletCount; } }
    //public float _maxBulletCount { get { return _limitBulletCount; } }

    public Transform _tfLookPos { get { return _posLook; } }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animControl = GetComponent<Animator>();
        _modelObj = transform.GetChild(0).gameObject;
        _prefabbullet = Resources.Load("Prefabs/Objects/BulletObject") as GameObject;

        _animControl.SetBool("IsBattle", true);

        InitializeData("Link", 500, 2, 1);
    }

    private void Start()
    {   
        _wndPlayerStatus = GameObject.Find("MiniAvatarWindow").GetComponent<MiniStatusWindow>();
        _wndPlayerStatus.InitSetting(_myName, 1, 1, 1);
    }

    private void Update()
    {
        //Debug.Log("1" + _nowAction);
        if (_isDead || _nowAction == eAniType.RELOAD
            || IngameManager._instance._nowGameState != IngameManager.eStateFlower.Play || isRewind)
            return;
        //Debug.Log("2" + _nowAction);

        Vector3 move = Vector3.zero;

        if (IngameManager._instance._firstView)
        {
            float mx, mz;
#if UNITY_EDITOR
            mx = Input.GetAxis("Horizontal");
            mz = Input.GetAxis("Vertical");
            move = transform.forward * mz;
#else
            mx = -_stickMovement._dirMoveFirst.x;
            mz = -_stickMovement._dirMoveFirst.y;
            mv = transform.forward * mz;
#endif
            if (_stickLauncher._isAimMotion)
            {
                Vector3 md = new Vector3(mx, 0, mz);
                md = (md.magnitude > 1) ? md.normalized : md;
                ChangeAnimationToDirectionFirstView(md);

                md = transform.TransformDirection(md);
                _controller.Move(md * _movSpeed * Time.deltaTime);
            }
            else
            {
                if (mz > 0)
                    ChangedAction(eAniType.RUN);
                else if(mz < 0)
                    ChangedAction(eAniType.WALK_BACK);
                else
                {
                    if (mx != 0)
                        ChangedAction(eAniType.WALK_BACK);
                    else
                        ChangedAction(eAniType.IDLE);
                }
                transform.Rotate(Vector3.up * mx * Time.deltaTime * 180);

                _controller.Move(move * _movSpeed * Time.deltaTime);
            }
        }
        else
        {
#if UNITY_EDITOR
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            move = new Vector3(vertical, 0, -horizontal);
            move = (move.magnitude > 1) ? move.normalized : move;

            //Vector3 move = _stickMovement._dirMov;
#else
        Vector3 move = _stickMovement._dirMov;
#endif
            if (_stickLauncher._isAimMotion)
            {
                //방향을 받아서 방향에 따른 애니메이션 변화
                ChangeAnimationToDirection(move);
            }
            else
            {//일반적인 이동시
                if (move.magnitude == 0)
                    ChangedAction(eAniType.IDLE);
                else if (move.magnitude > 0)
                {
                    ChangedAction(eAniType.RUN);
                    _modelObj.transform.rotation = Quaternion.LookRotation(move);
                }
            }

            _controller.Move(move * Time.deltaTime * _movSpeed);
        }
    }

    void ChangeAnimationToDirection(Vector3 dir)
    {
        if(dir == Vector3.zero)
        {//공격할 때
            if(_nowAction != eAniType.ATTACK)
            {
                ChangedAction(eAniType.ATTACK);
                _animControl.SetBool("StartAttack", true);
            }
            _modelObj.transform.rotation = Quaternion.LookRotation(_stickLauncher._direction);
        }
        else
        {//움직일 때
            InitializeDirection();
            if (dir.z == 0)
            {
                if (dir.x > 0) //왼쪽으로 움직이고 있을 때
                    ChangedAction(eAniType.WALK_LEFT);
                else if(dir.x < 0) //오른쪽으로 움직이고 있을 때
                    ChangedAction(eAniType.WALK_RIGHT);
            }
            else if(dir.z > 0) // 앞쪽으로 움직임이 있을 때
                ChangedAction(eAniType.RUN);
            else // 천천히 움직이고 있을 때
                ChangedAction(eAniType.WALK_BACK);
        }
    }

    void ChangeAnimationToDirectionFirstView(Vector3 dir)
    {
        if(dir.magnitude == 0)
        {
            if(_nowAction != eAniType.ATTACK)
            {
                ChangedAction(eAniType.ATTACK);
                _animControl.SetBool("StartAttack", true);
            }

            if (_stickLauncher._directionFirst.y >= 0.4f || _stickLauncher._directionFirst.y <= 0.4)
                transform.Rotate(_stickLauncher._directionFirst * Time.deltaTime);
        }
        else
        {
            if (dir.z == 0)
            {
                if (dir.x > 0)
                    ChangedAction(eAniType.WALK_LEFT);
                else if (dir.x < 0)
                    ChangedAction(eAniType.WALK_RIGHT);
            }
            else if (dir.z > 0)
                ChangedAction(eAniType.RUN);
            else
                ChangedAction(eAniType.WALK_BACK);
        }
    }

    public void InitializeDirection()
    {
        if(!IngameManager._instance._firstView)
            _modelObj.transform.rotation = Quaternion.identity;
    }

    public void Fire()
    {
        if (_nowAction == eAniType.RELOAD)
            return;

        GameObject go = Instantiate(_prefabbullet, _posFire.transform.position, _posFire.transform.rotation);
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.InitBulletData(this);
        _curBulletCount++;

        _wndPlayerStatus.SettingBulletSlider(_bulletRate);

        _animControl.SetBool("StartAttack", false);
        if (_curBulletCount >= _limitBulletCount)
            ChangedAction(eAniType.RELOAD);
    }

    public void EndReload()
    {
        _curBulletCount = 0;
        ChangedAction(eAniType.IDLE);
        _wndPlayerStatus.SettingBulletSlider(1);
    }

    public void ChangedAction(eAniType type)
    {
        switch(type)
        {
            case eAniType.IDLE:
            case eAniType.ATTACK:
                _animControl.SetInteger("AniType", (int)type);
                break;
            case eAniType.RUN:
                _movSpeed = _runSpeed;
                _animControl.SetInteger("AniType", (int)type);
                break;
            case eAniType.WALK:
            case eAniType.WALK_BACK:
            case eAniType.WALK_LEFT:
            case eAniType.WALK_RIGHT:
                _movSpeed = _walkSpeed;
                _animControl.SetInteger("AniType", (int)type);
                break;
            case eAniType.RELOAD:
                _animControl.SetTrigger("Reload");
                break;
            case eAniType.DEAD:
                _isDead = true;
                _animControl.SetTrigger("Dead");
                _controller.enabled = false;
                break;
        }

        _nowAction = type;
    }

    public bool OnHitting(int hitDamage)
    {
        if(!_isDead)
        {
            if (HittingMe(hitDamage))
            { // 죽었을 때
                ChangedAction(eAniType.DEAD);
            }
            else
            { // 살았을 때

            }

            _wndPlayerStatus.SettingHPSlider(_hpRate);
        }

        return _isDead;
    }

    public void SettingSticks()
    {
        _stickLauncher = GameObject.FindGameObjectWithTag("LauncherStick").GetComponent<StickObject>();
        _stickLauncher.SetOwnerPlayer(this);
        _stickMovement = GameObject.FindGameObjectWithTag("MoveStick").GetComponent<StickObject>();
        _stickMovement.SetOwnerPlayer(this);
    }
}
