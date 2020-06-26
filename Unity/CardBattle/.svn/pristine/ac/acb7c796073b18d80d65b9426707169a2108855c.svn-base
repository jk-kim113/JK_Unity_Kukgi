using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    int _stageNumber = 0;
    [SerializeField]
    SpriteRenderer _icon;
    [SerializeField]
    Transform _rootTrophy;
    [SerializeField]
    GameObject _lock;
#pragma warning restore

    private void Start()
    {
        InitStageInfo(UserInfoManager._instance._clearStage);
    }

    public void InitStageInfo(int clearStageNum)
    {
        _lock.SetActive(!(clearStageNum + 1 >= _stageNumber));
        if(clearStageNum >= _stageNumber)
        {// 클리어한 스테이지
            _icon.sprite = HomeSceneManager._instance.GetStageIcon(1);
            _rootTrophy.gameObject.SetActive(true);
        }
        else
        {// 클리어 하지 않은 스테이지
            _icon.sprite = HomeSceneManager._instance.GetStageIcon(0);
            _rootTrophy.gameObject.SetActive(false);
        }

        if(_rootTrophy.gameObject.activeSelf)
        {
            int count = HomeSceneManager._instance.GetStageClearRank(_stageNumber);
            for(int n = 0; n < _rootTrophy.childCount; n++)
            {
                SpriteRenderer sr = _rootTrophy.GetChild(n).GetComponent<SpriteRenderer>();
                if(n < count)
                {
                    sr.color = Color.yellow;
                }
                else
                {
                    sr.color = Color.black;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if(!_lock.activeSelf)
        {   
            if(!HomeSceneManager._instance._isOpenedStageWnd)
            {
                HomeSceneManager._instance.OpenMessaageWindow(_stageNumber);
            }
        }
    }
}
