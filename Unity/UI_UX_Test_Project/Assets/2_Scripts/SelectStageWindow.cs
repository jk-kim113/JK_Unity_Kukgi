using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageWindow : MonoBehaviour
{
    public enum eTypePhaseSelectButton
    {
        Previous,
        Next
    }

#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
    [SerializeField]
    GameObject _rootPhaseObj;
    [SerializeField]
    GameObject _rootStageObj;
    [SerializeField]
    GameObject[] _btnSelectPhase;
    [SerializeField]
    Image _btnEnter;
    [SerializeField]
    Color _selectBtnEnter;
#pragma warning restore

    Animator _animCtrl;

    Vector3 _originSizeBtnExit;
    Vector3 _originSizeBtnSelectPhase;

    int _currentPhase;

    PhaseSlot[] _arrPhaseSlot;
    StageSlot[] _arrStageSlot;

    bool _isOnPhaseSelectButton;
    bool _isUpState;
    eTypePhaseSelectButton _currentPhaseSelectType;

    Color _originBtnEnter;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
        _currentPhase = 0;
    }

    private void Start()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
        _originSizeBtnSelectPhase = _btnSelectPhase[0].transform.localScale;
        _arrPhaseSlot = _rootPhaseObj.GetComponentsInChildren<PhaseSlot>();
        _arrStageSlot = _rootStageObj.GetComponentsInChildren<StageSlot>();

        _originBtnEnter = _btnEnter.color;

        for (int n = 0; n < _arrStageSlot.Length; n++)
            _arrStageSlot[n].InitSlot(this, n);

        RearrangeStageSlot(_currentPhase);
    }

    private void Update()
    {
        if(_isOnPhaseSelectButton)
        {
            if(_isUpState)
            {
                _btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
                if (_btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale.x > _originSizeBtnSelectPhase.x * 1.2f)
                {
                    _isUpState = false;
                }
            }
            else
            {
                _btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
                if (_btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale.x < _originSizeBtnSelectPhase.x)
                {
                    _isUpState = true;
                }
            }
        }
    }

    void RearrangeStageSlot(int phaseNum)
    {
        if (phaseNum < 0)
            phaseNum = _currentPhase = 0;

        if (phaseNum >= _arrPhaseSlot.Length)
            phaseNum = _currentPhase = 0;

        for (int n = 0; n < _arrPhaseSlot.Length; n++)
            _arrPhaseSlot[n].OnOffSlot(false);

        _arrPhaseSlot[phaseNum].OnOffSlot(true);

        for (int n = 0; n < _arrStageSlot.Length; n++)
        {
            _arrStageSlot[n].WriteOderNumber((n + 1 + 6 * phaseNum).ToString());
            _arrStageSlot[n].NonSelectSlot();
        }   
    }

    public void SelectSlot(int id)
    {
        for (int n = 0; n < _arrStageSlot.Length; n++)
        {
            if (n == id)
                continue;
            _arrStageSlot[n].NonSelectSlot();
        } 
    }

    public void UpDownWindow()
    {
        _animCtrl.SetTrigger("Move");
    }

    public void DownEnterButton()
    {
        _btnEnter.color = _selectBtnEnter;
    }

    public void UpEnterButton()
    {
        _btnEnter.color = _originBtnEnter;
        SceneControlManager._instance.SceneChange("IngameScene");
    }

    #region Phase Select Button Function
    public void DownPhaseSelectButton(int type)
    {
        _currentPhaseSelectType = (eTypePhaseSelectButton)type;
        _isUpState = true;
        _isOnPhaseSelectButton = true;
    }

    public void UpPhaseSelectButton()
    {
        _isOnPhaseSelectButton = false;
        _btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale = _originSizeBtnSelectPhase;

        switch(_currentPhaseSelectType)
        {
            case eTypePhaseSelectButton.Previous:
                RearrangeStageSlot(_currentPhase - 1);
                break;
            case eTypePhaseSelectButton.Next:
                RearrangeStageSlot(_currentPhase + 1);
                break;
        }
    }
    #endregion

    #region Exit Button Trigger Function
    public void DownExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit * 1.2f;
    }

    public void UpExitButton()
    {
        _btnExit.transform.localScale = _originSizeBtnExit;
        _animCtrl.SetTrigger("Move");
    }
    #endregion
}
