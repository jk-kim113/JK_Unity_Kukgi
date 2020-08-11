using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _btnExit;
    [SerializeField]
    GameObject _rootPhaseObj;
    [SerializeField]
    GameObject _rootItemObj;
    [SerializeField]
    GameObject[] _btnSelectPhase;
    [SerializeField]
    Text _txtItemInfo;
    [SerializeField]
    Image _btnBuy;
    [SerializeField]
    Color _selectBtnBuy;
#pragma warning restore

    Animator _animCtrl;

    Vector3 _originSizeBtnExit;
    Vector3 _originSizeBtnSelectPhase;

    PhaseSlot[] _arrPhaseSlot;
    ItemSlot[] _arrItemSlot;

    int _currentPhase;
    int _currentSelectSlotID;

    bool _isOnPhaseSelectButton;
    bool _isUpState;
    SelectStageWindow.eTypePhaseSelectButton _currentPhaseSelectType;

    Color _originBtnBuy;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
        _currentPhase = 0;
        _currentSelectSlotID = -1;
    }

    private void Start()
    {
        _originSizeBtnExit = _btnExit.transform.localScale;
        _originSizeBtnSelectPhase = _btnSelectPhase[0].transform.localScale;

        _arrPhaseSlot = _rootPhaseObj.GetComponentsInChildren<PhaseSlot>();
        _arrItemSlot = _rootItemObj.GetComponentsInChildren<ItemSlot>();

        _originBtnBuy = _btnBuy.color;

        for (int n = 0; n < _arrItemSlot.Length; n++)
            _arrItemSlot[n].InitSlot(this, n);

        RearrangeSlot(_currentPhase);
    }

    private void Update()
    {
        if (_isOnPhaseSelectButton)
        {
            if (_isUpState)
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

    void RearrangeSlot(int phase)
    {
        if (phase < 0)
            phase = _currentPhase = 0;

        if (phase >= _arrPhaseSlot.Length)
            phase = _currentPhase = 0;

        for (int n = 0; n < _arrPhaseSlot.Length; n++)
            _arrPhaseSlot[n].OnOffSlot(false);

        _arrPhaseSlot[phase].OnOffSlot(true);

        for(int n = 0; n < _arrItemSlot.Length; n++)
            _arrItemSlot[n].InitSlot();

        int slotID = 0;

        for(int n = 0; n < ResourcePoolManager._instance._ItemDataDic.Count; n++)
        {
            ItemData item = ResourcePoolManager._instance._ItemDataDic[n];
            if (item._type == (ItemData.eTypeItem)phase)
                _arrItemSlot[slotID++].InsetItemData(null, n);
        }
    }

    public void SelectSlot(int id)
    {
        _currentSelectSlotID = id;
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

    public void ShowSelectItem(int id)
    {
        _txtItemInfo.text = string.Format(ResourcePoolManager._instance._ItemDataDic[id]._info, ResourcePoolManager._instance._ItemDataDic[id]._value);
    }

    public void DownBuyButton()
    {
        _btnBuy.color = _selectBtnBuy;
    }

    public void UpBuyButton()
    {
        _btnBuy.color = _originBtnBuy;
        if (_currentSelectSlotID < 0)
            return;
        ResourcePoolManager._instance.BuyItem(_arrItemSlot[_currentSelectSlotID]._ItemIndex);
    }

    #region Phase Select Button Function
    public void DownPhaseSelectButton(int type)
    {
        _currentPhaseSelectType = (SelectStageWindow.eTypePhaseSelectButton)type;
        _isUpState = true;
        _isOnPhaseSelectButton = true;
    }

    public void UpPhaseSelectButton()
    {
        _isOnPhaseSelectButton = false;
        _btnSelectPhase[(int)_currentPhaseSelectType].transform.localScale = _originSizeBtnSelectPhase;

        switch (_currentPhaseSelectType)
        {
            case SelectStageWindow.eTypePhaseSelectButton.Previous:
                RearrangeSlot(_currentPhase - 1);
                break;
            case SelectStageWindow.eTypePhaseSelectButton.Next:
                RearrangeSlot(_currentPhase + 1);
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
