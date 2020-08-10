using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    InputField _inputPW;
    [SerializeField]
    GameObject _btnEnter;
    [SerializeField]
    Color _btnEnterDownColor;
#pragma warning restore

    int _lengthPW;
    Vector3 _scaleBtnEnter;
    bool _isDownBtnEnter;
    Color _originColorBtnEnter;

    private void Start()
    {
        _lengthPW = _inputPW.text.Length;
        _scaleBtnEnter = _btnEnter.transform.localScale;
        _isDownBtnEnter = false;
        _originColorBtnEnter = _btnEnter.GetComponent<Image>().color;
    }

    private void Update()
    {
        if(_inputPW.isFocused)
        {
            if (_lengthPW != _inputPW.text.Length)
            {
                _lengthPW = _inputPW.text.Length;
                HidePassWord();
            }
        }

        if(_isDownBtnEnter)
        {
            _btnEnter.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
            if(_btnEnter.transform.localScale.x > _scaleBtnEnter.x * 1.2f)
            {
                _btnEnter.transform.localScale = _scaleBtnEnter * 1.2f;
            }
        }
    }

    void HidePassWord()
    {
        _inputPW.text = "";
        for (int n = 0; n < _lengthPW; n++)
            _inputPW.text += "*";
    }

    public void EnterDownButton()
    {
        _isDownBtnEnter = true;
        _btnEnter.GetComponent<Image>().color = _btnEnterDownColor;
    }

    public void EnterUpButton()
    {
        _isDownBtnEnter = false;
        _btnEnter.transform.localScale = _scaleBtnEnter;
        _btnEnter.GetComponent<Image>().color = _originColorBtnEnter;
    }

}
