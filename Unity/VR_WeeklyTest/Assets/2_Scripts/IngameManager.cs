using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public enum eDifficultyType
    {
        Hard        = 0,
        Middle,
        Easy,
        OK
    }

    eDifficultyType _prevDifficultType;
    eDifficultyType _currDifficultType;

    GateControl _gateCtrl;

    static IngameManager _uniqueInstance;
    public static IngameManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _gateCtrl = GameObject.Find("Gate").GetComponent<GateControl>();
    }

    public void StopPoint(int id)
    {
        switch(id)
        {
            case 2:
                _gateCtrl.OnOffMenuWindow(true);
                break;
        }
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
            _prevDifficultType = _currDifficultType;
            _currDifficultType = type;
        }
    }

    public void OpenOkButton()
    {
        _gateCtrl.OnOffOkbuton(true);
    }
}
