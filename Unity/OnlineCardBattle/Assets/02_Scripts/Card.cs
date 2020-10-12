using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
#pragma warning disable 0649
    [SerializeField]
    Image _backImg;
    [SerializeField]
    Image _frontImg;
    [SerializeField]
    Image _actionImg;
    [SerializeField]
    Sprite _originImg;
    [SerializeField]
    Sprite _selectImg;
    [SerializeField]
    Image _iconImg;
#pragma warning restore

    float _timeCheck;
    bool _isRotate = false;
    bool _isFront = false;
    Quaternion _targetAngle;

    bool _isSelect = false;

    int _idx;

    public bool _IsRotate { get { return _isRotate; } }

    private void Start()
    {
        _actionImg.gameObject.SetActive(false);

        ShowCard(false);
    }

    private void Update()
    {
        if (_isRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetAngle, Time.deltaTime * 360);

            _timeCheck += Time.deltaTime;
            if (_timeCheck > 0.25f)
            {
                if (_isFront)
                    ShowCard(true);
                else
                    ShowCard(false);
            }

            if (_timeCheck >= 0.7f)
            {
                _isRotate = false;
            }
        }
    }

    public void CardReverse()
    {
        _isSelect = false;
        Vector3 tEuler = Vector3.zero;
        _timeCheck = 0;
        _isRotate = true;
        if (_isFront)
            tEuler = new Vector3(0, 0, 0);
        else
            tEuler = new Vector3(0, 180, 0);

        _targetAngle = Quaternion.Euler(tEuler);

        _isFront = !_isFront;
    }

    void ShowCard(bool isFront)
    {
        _backImg.gameObject.SetActive(!isFront);
        _frontImg.gameObject.SetActive(isFront);
    }

    public void InitCard(int idx)
    {
        _idx = idx;
    }

    public void OriginBack()
    {
        _backImg.sprite = _originImg;
    }

    public void ShowIcon(Sprite img)
    {
        _iconImg.sprite = img;
    }

    public void CorrectMark()
    {
        _actionImg.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IngameManager._instance._IsMyTurn)
        {
            if(!_isSelect)
            {
                _backImg.sprite = _selectImg;
                IngameManager._instance.SelectCard(true, _idx);
            }
            else
            {
                _backImg.sprite = _originImg;
                IngameManager._instance.SelectCard(false, _idx);
            }

            _isSelect = !_isSelect;
        }
    }
}
