using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _btnStage;
    [SerializeField]
    Image _btnPlayerInfo;
    [SerializeField]
    Color _selectBtnPlayerInfo;
    [SerializeField]
    Image _btnShop;
    [SerializeField]
    Color _selectBtnShop;
#pragma warning restore

    SelectStageWindow _wndSelectStage;
    PlayerInfoWindow _wndPlayerInfo;
    ShopWindow _wndShop;

    Color _originBtnPlayerInfo;
    Color _originBtnShop;

    private void Start()
    {
        _wndSelectStage = GameObject.FindGameObjectWithTag("SelectStageWindow").GetComponent<SelectStageWindow>();
        _wndPlayerInfo = GameObject.FindGameObjectWithTag("PlayerInfoWindow").GetComponent<PlayerInfoWindow>();
        _wndShop = GameObject.FindGameObjectWithTag("ShopWindow").GetComponent<ShopWindow>();

        _originBtnPlayerInfo = _btnPlayerInfo.color;
        _originBtnShop = _btnShop.color;
    }

    public void StageDownButton(Sprite img)
    {
        _btnStage.sprite = img;
    }

    public void StageUpButton(Sprite img)
    {
        _btnStage.sprite = img;
        _wndSelectStage.UpDownWindow();
    }

    public void PlayerInfoDownButton()
    {
        _btnPlayerInfo.color = _selectBtnPlayerInfo;
    }

    public void PlayerInfoUpButton()
    {
        _btnPlayerInfo.color = _originBtnPlayerInfo;
        _wndPlayerInfo.UpDownWindow();
    }

    public void ShopDownButton()
    {
        _btnShop.color = _selectBtnShop;
    }

    public void ShopUpButton()
    {
        _btnShop.color = _originBtnShop;
        _wndShop.UpDownWindow();
    }

}
