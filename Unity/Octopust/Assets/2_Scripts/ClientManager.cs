using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    static ClientManager _uniqueInstance;
    public static ClientManager _instance { get { return _uniqueInstance; } }

#pragma warning disable 0649
    [SerializeField]
    GameObject _characSlot;
    [SerializeField]
    GameObject _selectCharac;
    [SerializeField]
    GameObject _selectName;
    [SerializeField]
    GameObject _panel;
#pragma warning restore

    CharacterSlot[] _characterSlots;

    Sprite[] _characterImgs;

    public Sprite[] _selectedImg { get { return _characterImgs; } }

    int _characterID;
    public int _characID { get { return _characterID; } }
    string _name;
    public string _nowName { get { return _name; } }

    private void Awake()
    {
        _uniqueInstance = this;

        _characterImgs = Resources.LoadAll<Sprite>("Images");

        _panel.SetActive(true);
        _selectCharac.SetActive(true);
        _selectName.SetActive(false);
    }

    private void Start()
    {
        _characterSlots = _characSlot.GetComponentsInChildren<CharacterSlot>();

        for (int n = 0; n < _characterSlots.Length; n++)
        {
            _characterSlots[n].InitSlot(_characterImgs[n], n);
        }
    }

    public void SelectCharacter(int characID)
    {
        _characterID = characID;
        _selectCharac.SetActive(false);
        _selectName.SetActive(true);
    }

    public void SelectName(string name)
    {
        _name = name;
        _panel.SetActive(false);
        TCPClient._instance.ChatStart();
    }
}
