using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatus : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _iconCharacter;
    [SerializeField]
    Text _nameCharacter;
    [SerializeField]
    Slider _barHP;
    //========= 
    //몬스터용
    [SerializeField]
    Text _txtCharacter;
    [SerializeField]
    Image _bgRank;
    [SerializeField]
    Color[] _monsterColor;
#pragma warning restore

    public void InitInfoData(Sprite icon, string name, float hprate, string charactxt = "", BaseStatus.eTypeRank rank = BaseStatus.eTypeRank.Normal)
    {
        gameObject.SetActive(true);

        if (icon != null)
            _iconCharacter.sprite = icon;

        if (_txtCharacter != null)
            _txtCharacter.text = charactxt;

        if (_bgRank != null)
            _bgRank.color = _monsterColor[(int)rank];

        _nameCharacter.text = name;
        SetHpRate(hprate);
    }

    public void SetHpRate(float rate)
    {
        _barHP.value = rate;
    }
}
