using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    UserInfo[] _infoSlot;
    [SerializeField]
    RectTransform _positionCardRoot;
    [SerializeField]
    Button _startBtn;
#pragma warning restore

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    string[] _otherName;

    const int _maxCardHorizontalCount = 6;

    GameObject _prefabCard;
    List<Card> _ltCardInfos = new List<Card>();
    int _cardCount = 24;

    bool _isMyTurn = false;
    public bool _IsMyTurn { get { return _isMyTurn; } }

    bool _isMaster = false;
    public bool _IsMaster { get { return _isMaster; } }

    bool _isTurnCard = false;
    public bool _IsTurnCard { get { return _isTurnCard; } }

    int[] _selectCardIdx = new int[2];

    int _selectNum;

    Sprite[] _iconImgArr;

    GameObject _resultWndObj;

    int _myIndex;
    public int _MyIndex { get { return _myIndex; } }

    private void Awake()
    {
        _uniqueInstance = this;

        ClientManager._instance._IsInLobby = false;
    }

    private void Start()
    {
        _prefabCard = Resources.Load("Prefabs/CardObject") as GameObject;
        _iconImgArr = Resources.LoadAll<Sprite>("Card");
        _resultWndObj = Resources.Load("Prefabs/GameResultWindow") as GameObject;

        _otherName = new string[_infoSlot.Length];

        for (int n = 0; n < _infoSlot.Length; n++)
            _infoSlot[n].InitInfo(n);

        ClientManager._instance._IsIngame = true;
    }

    public void InitIngame()
    {
        for (int n = 0; n < _ltCardInfos.Count; n++)
            Destroy(_ltCardInfos[n].gameObject);

        if(!_isMaster)
            ClientManager._instance.ReadyForGame();
        else
            _startBtn.interactable = false;
    }

    public void GameStart()
    {
        StartCoroutine(CreateCard());
    }

    IEnumerator CreateCard()
    {
        for (int n = 0; n < _cardCount; n++)
        {
            int xid = n % _maxCardHorizontalCount;
            int yid = n / _maxCardHorizontalCount;
            
            Card card = Instantiate(_prefabCard, _positionCardRoot).GetComponent<Card>();
            card.InitCard(n);
            card.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3((230f * xid), -(340f * yid));

            _ltCardInfos.Add(card);

            yield return new WaitForSeconds(.1f);
        }
    }

    public void SelectCard(bool isSelect, int idx)
    {
        if (isSelect)
        {
            _selectCardIdx[_selectNum++] = idx;

            if(_selectNum >= 2)
            {
                _selectNum = 0;
                _isMyTurn = false;
                ClientManager._instance.SelectComplete(_selectCardIdx);
            }
        }
        else
        {
            if (_selectNum > 0)
                _selectNum--;
        }
    }

    public void ReverseCard(int idx1, int idx2, int imgIdx1, int imgIdx2)
    {
        _isTurnCard = true;
        _ltCardInfos[idx1].ShowIcon(_iconImgArr[imgIdx1]);
        _ltCardInfos[idx2].ShowIcon(_iconImgArr[imgIdx2]);

        _ltCardInfos[idx1].CardReverse();
        _ltCardInfos[idx2].CardReverse();
    }

    public void CorrectCard(string name, int idx1, int idx2, bool isMy)
    {
        if(isMy)
        {
            _infoSlot[_myIndex].ShowCorrectNumber();
        }
        else
        {
            for (int n = 0; n < _otherName.Length; n++)
            {
                if (!string.IsNullOrEmpty(_otherName[n]))
                {
                    if (_otherName[n].Equals(name))
                    {
                        _infoSlot[n].ShowCorrectNumber();
                        break;
                    }
                }
            }
        }

        _ltCardInfos[idx1].CorrectMark();
        _ltCardInfos[idx2].CorrectMark();

        _isTurnCard = false;
    }

    public void InCorrectCard(int idx1, int idx2)
    {
        StartCoroutine(ReverseCardSafely(idx1, idx2));
    }

    IEnumerator ReverseCardSafely(int idx1, int idx2)
    {
        while(_ltCardInfos[idx1]._IsRotate && _ltCardInfos[idx1]._IsRotate)
        {
            yield return null;
        }

        _ltCardInfos[idx1].CardReverse();
        _ltCardInfos[idx2].CardReverse();
        _ltCardInfos[idx1].OriginBack();
        _ltCardInfos[idx2].OriginBack();

        while (_ltCardInfos[idx1]._IsRotate && _ltCardInfos[idx1]._IsRotate)
        {
            yield return null;
        }

        _isTurnCard = false;
    }

    public void ShowUserInfo(int index, string name, int avatarID, bool isMy)
    {
        if (_isMaster)
            _startBtn.interactable = false;

        if (!string.IsNullOrEmpty(_otherName[index]))
            if (_otherName[index].Equals(name))
                return;

        _otherName[index] = name;

        if (isMy)
        {
            _myIndex = index;

            _infoSlot[index].InitInfo(ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "Avatar" + (ClientManager._instance._MyAvatar + 1)),
                        ClientManager._instance._MyName);
        }
        else
        {
            _infoSlot[index].InitInfo(ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "Avatar" + (avatarID + 1)),
                        name);
        }
    }

    public void ShowAI(int index, string name)
    {
        _otherName[index] = name;

        _infoSlot[index].ShowAI(name);
    }

    public void ShowMaster(string name, bool isMaster)
    {
        _startBtn.interactable = !isMaster;
        _startBtn.gameObject.GetComponentInChildren<Text>().text = isMaster ? "게임시작" : "준비하기";

        _isMaster = isMaster;

        if (isMaster)
        {   
            for (int n = 0; n < _infoSlot.Length; n++)
            {
                if(n == _myIndex)
                    _infoSlot[n].ShowMaster(true);
                else
                    _infoSlot[n].ShowMaster(false);
            }
        }
        else
        {
            _infoSlot[_myIndex].ShowMaster(false);
            for (int n = 0; n < _infoSlot.Length; n++)
            {
                if (!string.IsNullOrEmpty(_otherName[n]))
                {
                    if (_otherName[n].Equals(name))
                        _infoSlot[n].ShowMaster(true);
                    else
                        _infoSlot[n].ShowMaster(false);
                }
            }
        }
    }

    public void ShowReady(int slotIndex)
    {
        if (_infoSlot[slotIndex]._IsReady)
            _infoSlot[slotIndex].ShowReady(false);
        else
            _infoSlot[slotIndex].ShowReady(true);
    }

    public void ShowExit(string name)
    {
        for(int n = 0; n < _infoSlot.Length; n++)
        {
            if (!string.IsNullOrEmpty(_otherName[n]))
                if (_otherName[n].Equals(name))
                {
                    _otherName[n] = string.Empty;
                    _infoSlot[n].ExitRoom();
                }   
        }
    }

    public void ShowTurn(string name, bool isMy)
    {
        _isMyTurn = isMy;
        if (isMy)
        {   
            for (int n = 0; n < _infoSlot.Length; n++)
            {
                if(n == _myIndex)
                    _infoSlot[n].ShowTurnIcon(true);
                else
                    _infoSlot[n].ShowTurnIcon(false);
            }
                
        }
        else
        {
            _infoSlot[_myIndex].ShowTurnIcon(false);
            for (int n = 0; n < _infoSlot.Length; n++)
            {
                if(!string.IsNullOrEmpty(_otherName[n]))
                {
                    if (_otherName[n].Equals(name))
                        _infoSlot[n].ShowTurnIcon(true);
                    else
                        _infoSlot[n].ShowTurnIcon(false);
                }
            }
        }
    }

    public void ShowGameResult(bool isWin)
    {
        GameResultWindow resultWnd = Instantiate(_resultWndObj).GetComponent<GameResultWindow>();
        resultWnd.ShowGameReslut(isWin ? "Win" : "Lose");
    }

    public void ReadyForGame()
    {
        if(!_isMaster)
            ClientManager._instance.ReadyForGame();
        else
            ClientManager._instance.GameStart();
    }

    public void CanPlay()
    {
        _startBtn.interactable = true;
    }

    public void ExitRoom()
    {
        ClientManager._instance._IsIngame = false;
        SceneManager.LoadScene("LobbyScene");
        ClientManager._instance.ExitRoom();
    }
}
