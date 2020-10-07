using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _img;
#pragma warning restore

    int _characterID;

    public void InitSlot(Sprite img, int characteID)
    {
        _img.sprite = img;
        _characterID = characteID;
    }

    public void ClickButton()
    {
        ClientManager._instance.SelectCharacter(_characterID);
    }
}
