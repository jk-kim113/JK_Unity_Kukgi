using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobyUIManager : MonoBehaviour
{
    static LobyUIManager _uniqueInstance;
    public static LobyUIManager _instance { get { return _uniqueInstance; } }

#pragma warning disable 0649
    [SerializeField]
    GameObject _rootStageBtn;
    [SerializeField]
    Text _textStageIndex;
    [SerializeField]
    ArrowButton[] _arrowBtnArr;
#pragma warning restore

    StageButton[] _stageBtnArr;
    int _currentDownStageBtnID = 0;
    int _currentPage = 1;

    public int _nowStage { get { return _currentDownStageBtnID; } }
    public int _nowEpi { get { return _currentPage; } }

    private void Awake()
    {
        _uniqueInstance = this;

        _stageBtnArr = _rootStageBtn.GetComponentsInChildren<StageButton>();
        for (int n = 0; n < _stageBtnArr.Length; n++)
            _stageBtnArr[n].SettingBtn(n + 1);
    }

    private void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Start()
    {   
        _currentPage = SaveDataManager._instance._nowSaveData._currentEpi;
        SetLobbyUI();
    }

    public void SetLobbyUI()
    {
        _textStageIndex.text = _currentPage.ToString();

        int id = 0;
        TableBase tb = TableManager._instance.Get(eTableType.Scenario);
        foreach(string key in tb._datas.Keys)
        {
            if (tb._datas[key]["Episode"].CompareTo(_currentPage.ToString()) == 0)
            {
                _stageBtnArr[id].OriginImage();
                _stageBtnArr[id++].WriteText(tb._datas[key]["StageName"]);
            }

            if (id >= _stageBtnArr.Length)
                break;
        }

        if (_currentPage == 1)
            _arrowBtnArr[(int)ArrowButton.eArrowType.Left].OffButton();

        if (_currentPage == SaveDataManager._instance._nowSaveData._currentEpi)
        {
            for (int n = SaveDataManager._instance._nowSaveData._currentStage + 1; n < _stageBtnArr.Length; n++)
                _stageBtnArr[n].GetComponent<Image>().color = Color.black;

            _arrowBtnArr[(int)ArrowButton.eArrowType.Right].OffButton();
        }   
        else
        {
            for (int n = 0; n < _stageBtnArr.Length; n++)
                _stageBtnArr[n].GetComponent<Image>().color = Color.white;
        }
    }

    public void NowClickStageBtn(int index)
    {
        if(_currentDownStageBtnID > 0)
            _stageBtnArr[_currentDownStageBtnID - 1].OriginImage();

        _currentDownStageBtnID = index;
    }

    public void MovePage(ArrowButton.eArrowType type)
    {
        for (int n = 0; n < _arrowBtnArr.Length; n++)
            _arrowBtnArr[n].OnButton();

        switch (type)
        {
            case ArrowButton.eArrowType.Left:
                _currentPage--;
                if (_currentPage <= 1)
                    _arrowBtnArr[(int)type].OffButton();
                break;
            case ArrowButton.eArrowType.Right:
                _currentPage++;
                if (_currentPage >= 10)
                    _arrowBtnArr[(int)type].OffButton();
                break;
        }

        SetLobbyUI();
    }
}
