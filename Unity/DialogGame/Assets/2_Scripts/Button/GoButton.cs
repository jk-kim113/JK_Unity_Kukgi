using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    public enum eSceneType
    {
        Start,
        Lobby,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    eSceneType _secneType;
#pragma warning restore

    Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Start()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "GoNormalBtn");
    }

    public void DownBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "GoDownBtn");
    }

    public void UpBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "GoNormalBtn");
        switch(_secneType)
        {
            case eSceneType.Start:
                StartManager._instance.SetPlayerName();
                break;
            case eSceneType.Lobby:
                LobbyManager._instance.GoStage();
                break;
        }
    }
}
