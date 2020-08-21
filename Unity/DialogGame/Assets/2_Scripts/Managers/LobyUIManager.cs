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

    private void Start()
    {
        SetLobbyUI(_currentPage);
    }

    public void SetLobbyUI(int stageIndex)
    {
        _textStageIndex.text = stageIndex.ToString();

        int id = 0;
        TableBase tb = TableManager._instance.Get(eTableType.Scenario);
        foreach(string key in tb._datas.Keys)
        {
            if (tb._datas[key]["Episode"].CompareTo(stageIndex.ToString()) == 0)
            {
                _stageBtnArr[id].OriginImage();
                _stageBtnArr[id++].WriteText(tb._datas[key]["StageName"]);
            }
                

            if (id >= _stageBtnArr.Length)
                break;
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
        switch (type)
        {
            case ArrowButton.eArrowType.Left:
                _currentPage--;
                if (_currentPage <= 0)
                    _currentPage = 1;
                break;
            case ArrowButton.eArrowType.Right:
                _currentPage++;
                if (_currentPage >= 11)
                    _currentPage = 10;
                break;
        }

        SetLobbyUI(_currentPage);
    }
}
