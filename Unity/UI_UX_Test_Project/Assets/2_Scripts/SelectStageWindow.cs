using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStageWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _rootPhaseObj;
    [SerializeField]
    GameObject _rootStageObj;
    [SerializeField]
    DownSizeChangeButton[] _btnSelectPhase;
    [SerializeField]
    Image _btnEnter;
    [SerializeField]
    Color _selectBtnEnter;
    [SerializeField]
    DownSizeUpButton _btnExit;
#pragma warning restore

    Animator _animCtrl;

    int _currentPhase;

    PhaseSlot[] _arrPhaseSlot;
    StageSlot[] _arrStageSlot;

    Color _originBtnEnter;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
        _currentPhase = 0;
    }

    private void Start()
    {
        _btnExit.InitButton(this);
        _arrPhaseSlot = _rootPhaseObj.GetComponentsInChildren<PhaseSlot>();
        _arrStageSlot = _rootStageObj.GetComponentsInChildren<StageSlot>();

        _originBtnEnter = _btnEnter.color;

        for (int n = 0; n < _btnSelectPhase.Length; n++)
            _btnSelectPhase[n].InitButton(this);

        for (int n = 0; n < _arrStageSlot.Length; n++)
            _arrStageSlot[n].InitSlot(this, n);

        _arrPhaseSlot[0].OnOffSlot(true);
        RearrangeStageSlot();
    }

    public override void ChangePhase(bool isNext)
    {
        if (isNext)
            _currentPhase++;
        else
            _currentPhase--;

        if (_currentPhase < 0)
            _currentPhase = _arrPhaseSlot.Length - 1;

        if (_currentPhase >= _arrPhaseSlot.Length)
            _currentPhase = 0;

        for (int n = 0; n < _arrPhaseSlot.Length; n++)
            _arrPhaseSlot[n].OnOffSlot(false);

        _arrPhaseSlot[_currentPhase].OnOffSlot(true);

        RearrangeStageSlot();
    }

    void RearrangeStageSlot()
    {
        for (int n = 0; n < _arrStageSlot.Length; n++)
        {
            _arrStageSlot[n].WriteOderNumber((n + 1 + 6 * _currentPhase).ToString());
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
        SceneManager.LoadScene("IngameScene");
    }

    

    public override void ExitButton()
    {
        _animCtrl.SetTrigger("Move");
    }
}
