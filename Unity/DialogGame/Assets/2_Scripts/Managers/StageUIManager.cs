using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    static StageUIManager _uniqueInstance;
    public static StageUIManager _instance { get { return _uniqueInstance; } }

#pragma warning disable 0649
    [SerializeField]
    Image _imgBG;
    [SerializeField]
    Image _imgCharacter;
    [SerializeField]
    Text _textNarration;
    [SerializeField]
    Text _textName;
    [SerializeField]
    Text _textStory;
    [SerializeField]
    Text _textEpisode;
#pragma warning restore

    Coroutine _writeTextCoroutine;

    private void Awake()
    {
        _uniqueInstance = this;
        _textStory.transform.parent.gameObject.SetActive(false);
        _imgCharacter.gameObject.SetActive(false);
    }

    public void SetBG(Sprite bg)
    {
        _imgBG.sprite = bg;
    }

    public void SetCharacter(Sprite charac)
    {
        if(charac == null)
            _imgCharacter.gameObject.SetActive(false);
        else
        {
            _imgCharacter.gameObject.SetActive(true);
            _imgCharacter.sprite = charac;
        }   
    }

    public void SetName(string name)
    {
        OnOffTextWnd(false);
        _textName.text = name;
    }

    public void SetEpisode(string epi)
    {
        _textEpisode.text = epi;
    }

    public void SetNotification(string notice)
    {
        OnOffTextWnd(true);
        _textNarration.text = notice;
    }

    public void SetNarration(string narration, bool isImmediate)
    {
        OnOffTextWnd(true);
        _textNarration.text = string.Empty;
        if (isImmediate)
            _textNarration.text = narration;
        else
            _writeTextCoroutine = StartCoroutine(WriteText(_textNarration, narration));
    }

    public void SetStory(string story, bool isImmediate)
    {
        OnOffTextWnd(false);
        _textStory.text = string.Empty;
        if (isImmediate)
            _textStory.text = story;
        else
            _writeTextCoroutine = StartCoroutine(WriteText(_textStory, story));
    }

    IEnumerator WriteText(Text text, string str)
    {
        text.text = string.Empty;
        int index = 0;
        char[] charArr = str.ToCharArray();

        WaitForSeconds term = new WaitForSeconds(.1f);

        while(index < charArr.Length)
        {
            yield return term;
            text.text += charArr[index++].ToString();
        }

        StageManager._instance._isCounting = true;
    }

    void OnOffTextWnd(bool isNarration)
    {
        _textNarration.transform.parent.gameObject.SetActive(isNarration);
        _textStory.transform.parent.gameObject.SetActive(!isNarration);
    }

    public void StopWrite()
    {
        StopCoroutine(_writeTextCoroutine);
    }
}
