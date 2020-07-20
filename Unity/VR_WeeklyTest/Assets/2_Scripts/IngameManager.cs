using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    ResultWindow _resultWnd;
#pragma warning restore

    public enum eDifficultyType
    {
        Hard        = 0,
        Middle,
        Easy,
        OK
    }

    public enum eTrackState
    {
        Start        = 1,
        SelectMenu,
        MonsterAppearance,
        Result
        
    }

    eDifficultyType _currDifficultType;
    eTrackState _currTrackState;
    public eTrackState _nowTrackState { get { return _currTrackState; } }

    GateControl _gateCtrl;
    MainUIControl _mainUICtrl;

    float _huntTime = 10.0f;

    int _killCount = 0;
    public int _killNum { get { return _killCount; } set { _killCount = value; } }

    int _deathCount = 0;
    public int _deathNum { get { return _deathCount; } set { _deathCount = value; } }

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _gateCtrl = GameObject.Find("Gate").GetComponent<GateControl>();
        _mainUICtrl = GameObject.Find("MainUI").GetComponent<MainUIControl>();
    }

    private void Update()
    {
        switch(_currTrackState)
        {
            case eTrackState.MonsterAppearance:

                _huntTime -= Time.deltaTime;
                _mainUICtrl.OnOffTimeText(true);
                _mainUICtrl.ShowTimeText(_huntTime);

                if(_huntTime < 0)
                {
                    _mainUICtrl.OnOffTimeText(false);
                    PlayerControl._isStop = false;
                }

                break;
        }
    }

    public void ActOnTrackPoint(int id)
    {
        switch ((eTrackState)id)
        {
            case eTrackState.Start:

                PlayerControl._isStop = false;

                break;
            case eTrackState.SelectMenu:

                PlayerControl._isStop = true;
                _gateCtrl.OnOffMenuWindow(true);

                break;
            case eTrackState.MonsterAppearance:

                PlayerControl._isStop = true;

                break;
            case eTrackState.Result:

                PlayerControl._isStop = true;
                _resultWnd.gameObject.SetActive(true);
                _resultWnd.OpenWnd(_killCount, _deathCount);

                break;
        }

        _currTrackState = (eTrackState)id;
    }

    public void SelectDifficulty(eDifficultyType type)  
    {
        if(type == eDifficultyType.OK)
        {
            _gateCtrl.OnOffMenuWindow(false);
            _gateCtrl.OpenGate();
            PlayerControl._isStop = false;
        }
        else
        {
            _currDifficultType = type;
        }
    }

    public void OpenOkButton()
    {
        _gateCtrl.OnOffOkbuton(true);
    }
}
