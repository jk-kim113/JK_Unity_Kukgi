using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBGUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _imgBG;
    [SerializeField]
    Text _textEpisode;
#pragma warning restore

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void InitSetting(Sprite bg, string epi)
    {
        _imgBG.sprite = bg;
        _textEpisode.text = epi;
    }
}
