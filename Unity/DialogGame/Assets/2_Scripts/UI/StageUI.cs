using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _imgCharacter;
    [SerializeField]
    Text _textNarration;
    [SerializeField]
    Text _textName;
    [SerializeField]
    Text _textStory;
#pragma warning restore

    Coroutine _writeTextCoroutine;
    string _currentText;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SetNotification(string str)
    {
        SettingTextBG(true);
        _textNarration.transform.parent.gameObject.GetComponent<Image>().raycastTarget = false;
        _textNarration.text = str;
    }

    public void SetDialog(Sprite img, bool isNarration, params string[] str)
    {
        if(_writeTextCoroutine != null)
            StopCoroutine(_writeTextCoroutine);

        _imgCharacter.sprite = img;

        SettingTextBG(isNarration);

        if (isNarration)
        {
            _currentText = str[0];
            _writeTextCoroutine = StartCoroutine(WriteText(_textNarration, str[0]));
        }
        else
        {
            _currentText = str[1];
            _textName.text = str[0];
            _writeTextCoroutine = StartCoroutine(WriteText(_textStory, str[1]));
        }
    }

    void SettingTextBG(bool isNarration)
    {
        _textNarration.transform.parent.gameObject.GetComponent<Image>().raycastTarget = true;
        _textNarration.transform.parent.gameObject.SetActive(isNarration);
        _textStory.transform.parent.gameObject.SetActive(!isNarration);
        _imgCharacter.gameObject.SetActive(!isNarration);
    }

    IEnumerator WriteText(Text text, string str)
    {   
        text.text = string.Empty;
        int index = 0;
        char[] charArr = str.ToCharArray();

        WaitForSeconds term = new WaitForSeconds(.1f);

        while (index < charArr.Length)
        {
            yield return term;
            text.text += charArr[index++].ToString();
        }

        StageManager._instance._isCounting = true;
    }

    public void WriteImmediately(bool isNarration)
    {
        StopCoroutine(_writeTextCoroutine);

        if (isNarration)
        {
            _textNarration.text = string.Empty;
            _textNarration.text = _currentText;
        }   
        else
        {
            _textStory.text = string.Empty;
            _textStory.text = _currentText;
        }   
    }

    public void DownNextBtn()
    {
        StageManager._instance.DownNextBtn();
    }
}
