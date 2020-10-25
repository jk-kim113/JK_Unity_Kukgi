using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubInfoWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _rootCharacSlots;
    [SerializeField]
    InputField _userNameField;
#pragma warning restore

    CharacterSlot[] _characterSlots;
    int _selectCharacIdx = -999;

    private void Start()
    {
        _characterSlots = _rootCharacSlots.GetComponentsInChildren<CharacterSlot>();
        for (int n = 0; n < _characterSlots.Length; n++)
        {
            _characterSlots[n].InitSlot(this ,ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image, "Avatar" + (n + 1)), n);
        }
    }

    public void SelectedCharac(int idx, bool isSelect)
    {
        if (isSelect)
        {
            _selectCharacIdx = idx;
            for (int n = 0; n < _characterSlots.Length; n++)
            {
                if (n != idx)
                    _characterSlots[n].OffSelect();
            }
        }
        else
        {
            _selectCharacIdx = -999;
            _characterSlots[idx].OffSelect();
        }
    }

    public void ClickEnterBtn()
    {
        if (!string.IsNullOrEmpty(_userNameField.text) && _selectCharacIdx >= 0)
        {   
            ClientManager._instance.MyInfo(_userNameField.text, _selectCharacIdx);
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
