using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateRoomWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _roomNameIF;
    [SerializeField]
    Toggle _pwAvailable;
    [SerializeField]
    InputField _pwIF;
    [SerializeField]
    Button _okBtn;
#pragma warning restore

    private void Start()
    {
        _pwAvailable.isOn = false;
        _pwIF.interactable = false;
    }

    private void Update()
    {
        _pwIF.interactable = _pwAvailable.isOn;
    }

    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(_roomNameIF.text))
        {
            ClientManager._instance.CreateRoom(_roomNameIF.text, _pwAvailable.isOn, _pwIF.text);
            SceneManager.LoadScene("IngameScene");
        }   
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}
