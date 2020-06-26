using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtExplan;
    [SerializeField]
    RectTransform _rtfYesBtn;
    [SerializeField]
    RectTransform _rtfNoBtn;
#pragma warning restore

    Vector2 _originYesPosition;
    Vector2 _originNoPosition;

    private void Start()
    {
        _originYesPosition = _rtfYesBtn.anchoredPosition;
        _originNoPosition = _rtfNoBtn.anchoredPosition;
    }

    public void OpenWindow(string explan)
    {
        gameObject.SetActive(true);
        _txtExplan.text = explan;
    }

    public void DownYesButton()
    {
        _rtfYesBtn.anchoredPosition = _originYesPosition + new Vector2(5, -10);
    }

    public void UpYesButton()
    {
        _rtfYesBtn.anchoredPosition = _originYesPosition;
    }

    public void ClickYesButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void DownNoButton()
    {

    }

    public void UpNoButton()
    {

    }

    public void ClickNoButton()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
