using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWindow : BaseWindow
{
#pragma warning disable 0649
    [SerializeField]
    Sprite _stageImg;
    [SerializeField]
    Transform _frameRoot;
#pragma warning restore

    int _pageNum;
    int _selectNum;

    List<SlotUI> _StageSlotList = new List<SlotUI>();

    public void OpenWindow()
    {
        ListUpSlot();

        bool first = false;
        for (int n = 0; n < _StageSlotList.Count; n++)
        {
            _StageSlotList[n].InitIcon(_stageImg, this, n + 1, first);
            //first = true;
        }

        gameObject.SetActive(true);
    }

    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }

    public override void SelectAllCheck(int no)
    {
        _selectNum = no;
        for (int n = 0; n < _StageSlotList.Count; n++)
        {
            if (no == n + 1)
                continue;
            _StageSlotList[n].DisableSelect();
        }
    }

    void ListUpSlot()
    {
        for (int n = 0; n < _frameRoot.childCount; n++)
        {
            SlotUI su = _frameRoot.GetChild(n).GetComponent<SlotUI>();
            _StageSlotList.Add(su);
        }
    }

    public void ClickStartBtn()
    {
        SceneControlManager._instance.StartSceneIngame(_selectNum);
    }
}
