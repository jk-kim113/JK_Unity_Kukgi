using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _textEpisode;
#pragma warning restore

    Image _img;
    int _index;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Start()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "StageBarNormalBtn");
    }

    public void ClickBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "StageBarSelectBtn");

        LobyUIManager._instance.NowClickStageBtn(_index);
    }

    public void OriginImage()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "StageBarNormalBtn");
    }

    public void SettingBtn(int index)
    {
        _index = index;
    }

    public void WriteText(string epi)
    {
        _textEpisode.text = epi;
    }
}
