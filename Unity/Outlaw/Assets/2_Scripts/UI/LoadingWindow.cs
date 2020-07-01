using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Slider _loadingBar;
    [SerializeField]
    Text _textLoadingTarget;
#pragma warning restore

    public void ShowLoadingBar(float value)
    {
        _loadingBar.value = value;
    }

    public void ShowLoadingTarget(string str)
    {
        _textLoadingTarget.text = str;
    }
}
