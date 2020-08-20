using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Start()
    {
        _img.sprite = ResourcePoolManager._instance.GetImage("GoNormalBtn");
    }

    public void DownBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetImage("GoDownBtn");
    }

    public void UpBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetImage("GoNormalBtn");
        LobbyManager._instance.GoStage();
    }
}
