﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    SpriteRenderer _icon;
    [SerializeField]
    SpriteRenderer _marking;
    [SerializeField]
    SpriteRenderer _backFever;
    [SerializeField]
    SpriteRenderer _lockImg;
#pragma warning restore

    SpriteRenderer _bg;

    bool _isRotate = false;
    bool _isFront = false;
    bool _isLock = false;
    bool _isCoupleLock = false;
    Quaternion _targetAngle;
    float _timeCheck = 0;

    // 카드 정보.
    int _no;
    int _iconIndex;

    public int _ICON_INDEX { get { return _iconIndex; } }

    public bool _isUsed { get { return _marking.gameObject.activeSelf; } }
    public bool _isFeverState { get { return _backFever.gameObject.activeSelf; } }
    public bool _isLockState { get { return _lockImg.gameObject.activeSelf; } }
    public bool _isCoupleLockState { get { return _isCoupleLock; } set { _isCoupleLock = value; } }

    private void Start()
    {
        _bg = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {   
        if(_isRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetAngle, Time.deltaTime * 360);

            _timeCheck += Time.deltaTime;
            if(_timeCheck > 0.25f)
            {
                if (_isFront)
                {
                    _bg.sortingOrder = 0;
                }   
                else
                    _bg.sortingOrder = 10;
            }

            if (_timeCheck >= 0.7f)
            {
                _isRotate = false;
            }
        }
        else
        {
            if (_backFever.gameObject.activeSelf)
            {
                if (_backFever.color.a < 1)
                    _backFever.color = new Color(_backFever.color.r, _backFever.color.g, _backFever.color.b, _backFever.color.a + .4f * Time.deltaTime);
            }
        }

        if(_lockImg.gameObject.activeSelf)
        {   
            if (_lockImg.transform.localScale.x <= 1 && _lockImg.transform.localScale.y <= 1)
            {
                _lockImg.transform.localScale = new Vector3(1, 1, 1);
                if(IngameManager._instance._nowGameState == IngameManager.eTypeGameState.LockCard)
                    IngameManager._instance.GamePlay();
            }
            else
            {
                _lockImg.transform.localScale = new Vector3(_lockImg.transform.localScale.x - 1.5f * Time.deltaTime,
                                                            _lockImg.transform.localScale.y - 1.5f * Time.deltaTime, 1);
            }
        }
    }

    public void InitData(int nummber, int iconId, Sprite icon)
    {
        _no = nummber;
        _iconIndex = iconId;
        _marking.gameObject.SetActive(false);
        ChangeIconImage(icon);
    }

    public void ChangeIconImage(Sprite icon)
    {
        _icon.sprite = icon;
    }

    void CardReverse()
    {   
        Vector3 tEuler = Vector3.zero;
        _timeCheck = 0;
        _isRotate = true;
        if (_isFront)
            tEuler = new Vector3(0, 0, 0);
        else
            tEuler = new Vector3(0, 180, 0);

        _targetAngle = Quaternion.Euler(tEuler);

        _isFront = !_isFront;
        _backFever.gameObject.SetActive(false);
    }

    /// <summary>
    /// 선택이 끝나고 체크까지 확인된 후 결과를 실행하는 함수.
    /// </summary>
    /// <param name="isMarking">맞췄는지를 받아온다.(맞추면 true)</param>
    public void EndMarking(bool isMarking)
    {
        if(isMarking)
        {
            _marking.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
            CardReverse();
    }

    public void FadeInFeverBack()
    {   
        _backFever.gameObject.SetActive(true);
    }

    public void LockCard()
    {
        _lockImg.transform.localScale = new Vector3(3, 3, 1);
        _lockImg.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!IngameManager._instance._isCardAction || _isLockState)
            return;

        if(IngameManager._instance._nowGameState == IngameManager.eTypeGameState.GamePlay)
        {
            if (!_isRotate)
            {   
                if (IngameManager._instance.CheckSelectCard(_no))
                    CardReverse();
            }
        }
    }
}
