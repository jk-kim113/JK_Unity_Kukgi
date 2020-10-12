using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    UserInfo _myInfo;
    [SerializeField]
    UserInfo[] _otherInfo;
    [SerializeField]
    RectTransform _positionCardRoot;
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

    bool _isTurnCard = false;
    public bool _IsTurnCard { get { return _isTurnCard; } }

    int[] _selectCardIdx = new int[2];

    int _selectNum;

    Sprite[] _iconImgArr;

    GameObject _resultWndObj;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _prefabCard = Resources.Load("Prefabs/CardObject") as GameObject;
        _iconImgArr = Resources.LoadAll<Sprite>("Card");
        _resultWndObj = Resources.Load("Prefabs/GameResultWindow") as GameObject;

        _otherName = new string[_otherInfo.Length];

        _myInfo.InitInfo(
            ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "Avatar" + (ClientManager._instance._MyAvatar + 1)),
            ClientManager._instance._MyName);

        for(int n = 0; n < _otherInfo.Length; n++)
        {
            _otherInfo[n].gameObject.SetActive(false);
        }

        ClientManager._instance._IsIngame = true;
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
            _myInfo.ShowCorrectNumber();
        }
        else
        {
            for (int n = 0; n < _otherName.Length; n++)
            {
                if (!string.IsNullOrEmpty(_otherName[n]))
                {
                    if (_otherName[n].Equals(name))
                    {
                        _otherInfo[n].ShowCorrectNumber();
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

    public void ShowOtherUser(string name, int avatarID)
    {
        for(int n = 0; n < _otherName.Length; n++)
        {
            if(!string.IsNullOrEmpty(_otherName[n]))
                if (_otherName[n].Equals(name))
                    return;
        }

        for(int n = 0; n < _otherInfo.Length; n++)
        {   
            if(!_otherInfo[n]._IsConnect)
            {
                _otherName[n] = name;
                _otherInfo[n].gameObject.SetActive(true);
                _otherInfo[n].InitInfo(
                    ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "Avatar" + (avatarID + 1)),
                    name);
                break;
            }
        }
    }

    public void ShowTurn(string name, bool isMy)
    {
        _isMyTurn = isMy;
        if (isMy)
        {
            _myInfo.ShowTurnIcon(true);
            for (int n = 0; n < _otherInfo.Length; n++)
                _otherInfo[n].ShowTurnIcon(false);
        }
        else
        {
            _myInfo.ShowTurnIcon(false);
            for (int n = 0; n < _otherInfo.Length; n++)
            {
                if(!string.IsNullOrEmpty(_otherName[n]))
                {
                    if (_otherName[n].Equals(name))
                        _otherInfo[n].ShowTurnIcon(true);
                    else
                        _otherInfo[n].ShowTurnIcon(false);
                }
            }
        }
    }

    public void ShowGameResult(bool isWin)
    {
        GameResultWindow resultWnd = Instantiate(_resultWndObj).GetComponent<GameResultWindow>();
        resultWnd.ShowGameReslut(isWin ? "Win" : "Lose");
    }
}
