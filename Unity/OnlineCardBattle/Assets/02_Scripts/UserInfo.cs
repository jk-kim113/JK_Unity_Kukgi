using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _avatarImg;
    [SerializeField]
    Text _nameTxt;
    [SerializeField]
    Text _correctNumTxt;
    [SerializeField]
    Image _turnImg;
#pragma warning restore

    bool _isConnect = false;
    public bool _IsConnect { get { return _isConnect; } }

    int correctNum = 0;

    private void Start()
    {
        _correctNumTxt.text = "0";
        ShowTurnIcon(false);
    }

    public void InitInfo(Sprite img, string name)
    {
        _avatarImg.sprite = img;
        _nameTxt.text = name;
        _isConnect = true;
    }

    public void ShowCorrectNumber()
    {
        _correctNumTxt.text = (++correctNum).ToString();
    }

    public void ShowTurnIcon(bool isMyTurn)
    {
        _turnImg.gameObject.SetActive(isMyTurn);
    }
}
