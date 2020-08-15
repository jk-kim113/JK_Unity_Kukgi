using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSizeChangeButton : MonoBehaviour
{
    public enum eTypePhaseSelectButton
    {
        Previous,
        Next
    }

#pragma warning disable 0649
    [SerializeField]
    eTypePhaseSelectButton _currentPhaseSelectType;
    [SerializeField]
    float _changeRate = 0.2f;
#pragma warning restore

    BaseWindow _owner;
    Vector3 _originSizeBtnSelectPhase;
    bool _isOnPhaseSelectButton;
    bool _isUpState;

    private void Awake()
    {
        _originSizeBtnSelectPhase = transform.localScale;
    }

    private void Update()
    {
        if (_isOnPhaseSelectButton)
        {
            if (_isUpState)
            {
                transform.localScale += Vector3.one * _changeRate * Time.deltaTime;
                if (transform.localScale.x > _originSizeBtnSelectPhase.x * 1.2f)
                    _isUpState = false;
            }
            else
            {
                transform.localScale -= Vector3.one * _changeRate * Time.deltaTime;
                if (transform.localScale.x < _originSizeBtnSelectPhase.x)
                    _isUpState = true;
            }
        }
    }

    public void InitButton(BaseWindow owner)
    {
        _owner = owner;
    }

    public void DownPhaseSelectButton()
    {   
        _isUpState = true;
        _isOnPhaseSelectButton = true;
    }

    public void UpPhaseSelectButton()
    {
        _isOnPhaseSelectButton = false;
        transform.localScale = _originSizeBtnSelectPhase;

        switch (_currentPhaseSelectType)
        {
            case eTypePhaseSelectButton.Previous:
                _owner.ChangePhase(false);
                break;
            case eTypePhaseSelectButton.Next:
                _owner.ChangePhase(true);
                break;
        }
    }
}
