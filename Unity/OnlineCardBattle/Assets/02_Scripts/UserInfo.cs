using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _avatarImg;
    [SerializeField]
    Text _nameTxt;
    [SerializeField]
    Text _correctNumTxt;
    [SerializeField]
    Image _turnImg;
    [SerializeField]
    GameObject _emptySlot;
    [SerializeField]
    GameObject _connectSlot;
    [SerializeField]
    Sprite _aiImg;
#pragma warning restore

    bool _isConnect = false;
    public bool _IsConnect { get { return _isConnect; } }

    int _correctNum = 0;
    int _index;
    bool _isReady;
    public bool _IsReady { get { return _isReady; } }

    private void Awake()
    {
        ConnectRoom(false);
    }

    private void Start()
    {
        _correctNumTxt.text = "0";
        ShowTurnIcon(false);
    }

    void ConnectRoom(bool isConnect)
    {
        _emptySlot.SetActive(!isConnect);
        _connectSlot.SetActive(isConnect);
    }

    public void InitInfo(int index)
    {
        _index = index;
    }

    public void InitInfo(Sprite img, string name)
    {
        _avatarImg.sprite = img;
        _nameTxt.text = name;
        _isConnect = true;

        ConnectRoom(true);
    }

    public void ShowAI(string name)
    {
        _avatarImg.sprite = _aiImg;
        _isConnect = true;
        _nameTxt.text = name;
        ShowTurnIcon(false);

        ConnectRoom(true);
    }

    public void ShowCorrectNumber()
    {
        _correctNumTxt.text = (++_correctNum).ToString();
    }

    public void ShowTurnIcon(bool isMyTurn)
    {
        _turnImg.gameObject.SetActive(isMyTurn);
    }

    public void ShowMaster(bool isMaster)
    {
        if(isMaster)
            _nameTxt.color = Color.blue;
        else
            _nameTxt.color = Color.black;
    }

    public void ShowReady(bool isReady)
    {
        _isReady = isReady;

        if (isReady)
            _nameTxt.color = Color.yellow;
        else
            _nameTxt.color = Color.black;
    }

    public void ExitRoom()
    {
        _isConnect = false;
        ConnectRoom(false);
    }

    public void ClickAddAIBtn()
    {
        if (IngameManager._instance._IsMaster)
            ClientManager._instance.AddAI(_index);
    }
}
