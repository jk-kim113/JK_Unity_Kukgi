using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMessage : MonoBehaviour
{
    Text _txtMessage;

    private void Start()
    {
        _txtMessage = transform.GetChild(1).GetComponent<Text>();
    }

    public void EnableInfoMessage(bool isOn = false, string msg = "")
    {
        OnOffObject(isOn);
        SetInfoMessage(msg);
    }

    /// <summary>
    /// 정보 메세지를 끄고 키는 기능.
    /// </summary>
    /// <param name="isOn">true면 Active된다.</param>
    void OnOffObject(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    /// <summary>
    /// 문자열을 바꾸는 기능.
    /// </summary>
    /// <param name="msg">화면에 출력할 문자열</param>
    void SetInfoMessage(string msg)
    {
        if (_txtMessage == null)
            _txtMessage = transform.GetChild(0).GetComponent<Text>();
        _txtMessage.text = msg;
    }

}
