using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    public void DownBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetImage("BackDownBtn");
    }

    public void UpBtn()
    {
        _img.sprite = ResourcePoolManager._instance.GetImage("BackNormalBtn");
        SceneManager.LoadScene("LobbyScene");
    }
}
