using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Sprite[] _stageIcons;
    [SerializeField]
    GameObject _prefabMessageWnd;
#pragma warning restore

    GameObject _btnOption;
    GameObject _btnCharacter;
    RectTransform _btnShop;
    Vector2 _originShopBtnPos;
    MessageWindow _wndMessage;

    StageControl[] _stageControls;

    public bool _isOpenedStageWnd { get { return (_wndMessage == null) ? false : _wndMessage.gameObject.activeSelf; } }
    
    static HomeSceneManager _uniqueInstance;
    public static HomeSceneManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Update()
    {
        if(_btnOption != null)
        {
            Quaternion target = Quaternion.Euler(new Vector3(0, 0, 180));
            _btnOption.transform.rotation = Quaternion.RotateTowards(_btnOption.transform.rotation, target, Time.deltaTime * 180);
        }
    }

    public Sprite GetStageIcon(int number)
    {
        return (number < _stageIcons.Length) ? _stageIcons[number] : null;
    }

    public int GetStageClearRank(int stageNum)
    {
        return UserInfoManager._instance.GetTrophyCount(stageNum);
    }

    public void OpenMessaageWindow(int stageNum)
    {
        if(_wndMessage == null)
        {
            GameObject go = Instantiate(_prefabMessageWnd);
            _wndMessage = go.GetComponent<MessageWindow>();
            _wndMessage.OpenWindow(stageNum);
        }
        else
        {
            _wndMessage.OpenWindow(stageNum);
        }
    }

    #region "HomeScene메인 메뉴 버튼 액션들..."
    public void OptionDownButton(GameObject my)
    {
        _btnOption = my;
    }

    public void OptionUpButton(GameObject my)
    {
        if(_btnOption == my)
        {
            _btnOption.transform.rotation = Quaternion.identity;
            _btnOption = null;
        }
    }

    public void CharacterDownButton(GameObject my)
    {
        _btnCharacter = my;
        _btnCharacter.transform.localScale = new Vector3(0.8f, 0.8f, 1f); 
    }

    public void CharacterUpButton(GameObject my)
    {
        if(_btnCharacter == my)
        {
            _btnCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
            _btnCharacter = null;
        }
    }

    public void ShopDownButton(RectTransform my)
    {
        _btnShop = my;
        _originShopBtnPos = _btnShop.anchoredPosition;
        _btnShop.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        _btnShop.anchoredPosition += new Vector2(0, -30);
    }

    public void ShopUpButton(RectTransform my)
    {
        if (_btnShop == my)
        {
            _btnShop.transform.localScale = new Vector3(1f, 1f, 1f);
            _btnShop.anchoredPosition = _originShopBtnPos;
            _btnShop = null;
        }
    }
    #endregion

}
