using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _rootPhaseObj;
    [SerializeField]
    GameObject _rootItemObj;
    [SerializeField]
    DownSizeChangeButton[] _btnSelectPhase;
    [SerializeField]
    Text _txtItemInfo;
    [SerializeField]
    Image _btnBuy;
    [SerializeField]
    Color _selectBtnBuy;
    [SerializeField]
    DownSizeUpButton _btnExit;
    [SerializeField]
    GameObject _messageBought;
#pragma warning restore

    Animator _animCtrl;

    PhaseSlot[] _arrPhaseSlot;
    ItemSlot[] _arrItemSlot;

    int _currentPhase;

    Color _originBtnBuy;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
        _currentPhase = 0;
    }

    private void Start()
    {
        _btnExit.InitButton(this);

        _arrPhaseSlot = _rootPhaseObj.GetComponentsInChildren<PhaseSlot>();
        _arrItemSlot = _rootItemObj.GetComponentsInChildren<ItemSlot>();

        _originBtnBuy = _btnBuy.color;

        for (int n = 0; n < _btnSelectPhase.Length; n++)
            _btnSelectPhase[n].InitButton(this);

        for (int n = 0; n < _arrItemSlot.Length; n++)
            _arrItemSlot[n].InitSlot(this, n);

        _arrPhaseSlot[0].OnOffSlot(true);
        RearrangeSlot();
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

        RearrangeSlot();
    }

    void RearrangeSlot()
    {
        for(int n = 0; n < _arrItemSlot.Length; n++)
            _arrItemSlot[n].InitSlot();
    }

    public void SelectSlot(int id)
    {
        for (int n = 0; n < _arrItemSlot.Length; n++)
        {
            if (n == id)
                continue;
            _arrItemSlot[n].NonSelectSlot();
        }
    }

    public void UpDownWindow()
    {
        _animCtrl.SetTrigger("Move");
    }

    public void DownBuyButton()
    {
        _btnBuy.color = _selectBtnBuy;
    }

    public void UpBuyButton()
    {
        _btnBuy.color = _originBtnBuy;
        _messageBought.SetActive(true);
    }

    public override void ExitButton()
    {
        _animCtrl.SetTrigger("Move");
    }
}
