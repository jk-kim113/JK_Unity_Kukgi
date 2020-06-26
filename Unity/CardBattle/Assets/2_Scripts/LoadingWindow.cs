using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtLoading;
    [SerializeField]
    Slider _loadingBar;
    [SerializeField]
    Text _txtTip;
    [SerializeField]
    string[] _tipStrings;
#pragma warning restore

    float _timeCheck;
    int _dotCount;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        _timeCheck += Time.deltaTime;
        if(_timeCheck >= 0.3f)
        {
            _timeCheck = 0;
            _dotCount++;
            if (_dotCount > 6)
                _dotCount = 0;
            _txtLoading.text = "Loading.";
            for(int n = 0; n < _dotCount; n++)
                _txtLoading.text += ".";
        }
    }

    public void OpenLoadingWnd()
    {
        _txtTip.text = _tipStrings[Random.Range(0, _tipStrings.Length)];
        _loadingBar.value = 0;
    }

    public void SettingLoadRate(float rate)
    {
        _loadingBar.value = rate;
    }
}
