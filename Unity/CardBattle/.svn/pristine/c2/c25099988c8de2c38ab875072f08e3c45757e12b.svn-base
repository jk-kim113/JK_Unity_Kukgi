using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoWnd : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtPlayCount;
    [SerializeField]
    Text _txtNowMonster;
    [SerializeField]
    Text _txtMaxMonster;
    [SerializeField]
    Text _txtNowFail;
    [SerializeField]
    Text _txtMaxFail;
#pragma warning restore

    public void InitSetting()
    {
        _txtPlayCount.text = "0";
        _txtNowMonster.text = "0";
        _txtMaxMonster.text = "0";
        _txtNowFail.text = "0";
        _txtMaxFail.text = "0";
    }

    public void SetPlayerCount(int count)
    {
        _txtPlayCount.text = count.ToString();
    }

    public void SetPlayMonsterCount(int now, int max)
    {
        _txtNowMonster.text = now.ToString();
        _txtMaxMonster.text = max.ToString();
    }

    public void SetUntilDamageCount(int now, int max)
    {
        _txtNowFail.text = now.ToString();
        _txtMaxFail.text = max.ToString();
    }
}
