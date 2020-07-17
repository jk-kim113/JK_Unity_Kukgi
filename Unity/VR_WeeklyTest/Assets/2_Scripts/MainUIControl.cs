using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _timeObj;
    [SerializeField]
    Text _timeText;
#pragma warning restore

    public void OnOffTimeText(bool isOpen)
    {
        _timeObj.SetActive(isOpen);
    }

    public void ShowTimeText(float time)
    {
        _timeText.text = string.Format("{0} : {1}", (int)time, (int)((time - (int)time) * 100));
    }

}
