using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    float _durSpeed = 0.2f;     // 레티클이 커지는 속도
    [SerializeField]
    float _minSize = 0.4f;      // 레티클 최소 크기(최초 크기)
    [SerializeField]
    float _maxSize = 0.6f;      // 레티클 최대 크기

    Image _crossHair;
    float _timeStart;           // 레티클이 커지기 시작하는 시간을 저장.

    Animation _anim;

    static bool _isGazzing = false;
    public static bool _isGaze { get { return _isGazzing; } set { _isGazzing = value; } }

    private void Awake()
    {
        _crossHair = GetComponent<Image>();
        _timeStart = Time.time;

        _anim = GetComponent<Animation>();

        // 레티클 시작 크기 설정
        transform.localScale = Vector3.one * _minSize;
    }

    bool _isAnim;

    private void Update()
    {
        if(_isGazzing)
        {
            if(!_isAnim)
                _anim.Play("Gaze");

            _isAnim = true;
            //float t = (Time.time - _timeStart) / _durSpeed;
            //_crossHair.color = Color.red;
            //transform.localScale = Vector3.one * Mathf.Lerp(_minSize, _maxSize, t);
        }
        else
        {
            if (_isAnim)
                _anim.Play("NotGaze");

            _isAnim = false;
            //_crossHair.color = Color.white;
            //transform.localScale = Vector3.one * _minSize;
            //_timeStart = Time.time;
        }
    }
}
