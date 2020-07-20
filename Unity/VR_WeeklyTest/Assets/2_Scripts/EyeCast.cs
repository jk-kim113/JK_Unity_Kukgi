using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EyeCast : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    float _dist = 10.0f;
    [SerializeField]
    float _timeSelect = 1.0f;
#pragma warning restore

    Ray _ray;
    RaycastHit _hit;

    GameObject _prevButton;         // 이전 응시했던 버튼
    GameObject _currButton;         // 현재 응시하는 버튼

    Image _circleBar;
    float _timePass;
    bool _isClicked;

    private void Awake()
    {
        _timePass = 0;
        _isClicked = false;
    }

    private void Update()
    {
        _ray = new Ray(transform.position, transform.forward * _dist);

        Debug.DrawRay(_ray.origin, _ray.direction * _dist, Color.red);

        int layMask = 1 << LayerMask.NameToLayer("ITEMBOX") | 1 << LayerMask.NameToLayer("MENU");

        if (Physics.Raycast(_ray, out _hit, _dist, layMask))
        {
            PlayerControl._isStop = true;
            CrossHair._isGaze = true;
        }
        else
        {
            //playercontrol._isstop = false;
            CrossHair._isGaze = false;
        }

        CheckGazeButton();
    }

    void CheckGazeButton()
    {
        // 포인터 이벤트 정보 추출 변수
        PointerEventData data = new PointerEventData(EventSystem.current);

        int layMask = 1 << LayerMask.NameToLayer("MENU");
        if (Physics.Raycast(_ray, out _hit, _dist, layMask))
        {
            _currButton = _hit.collider.gameObject;
            _circleBar = _currButton.transform.GetChild(1).GetComponent<Image>();

            if (_currButton != _prevButton)
            {
                _timePass = 0;
                _circleBar.fillAmount = 0;
                _isClicked = false;

                if (_prevButton != null)
                {
                    _prevButton.transform.GetChild(1).GetComponent<Image>().fillAmount = 0;
                }

                // 현재 버튼에 pointerEnter를 전달
                ExecuteEvents.Execute(_currButton, data, ExecuteEvents.pointerEnterHandler);
                // 이전 버튼에 pointerExit를 전달
                ExecuteEvents.Execute(_prevButton, data, ExecuteEvents.pointerExitHandler);
                _prevButton = _currButton;
            }
            else if (!_isClicked)
            {// 클릭은 되지 않았으나 같은 메뉴를 응시 하고 있는 경우
                _timePass += Time.deltaTime;
                _circleBar.fillAmount = _timePass / _timeSelect;

                if (_timePass >= _timeSelect)
                {
                    ExecuteEvents.Execute(_currButton, data, ExecuteEvents.pointerClickHandler);
                    IngameManager._instance.OpenOkButton();
                    _isClicked = true;
                }
            }
        }
        else
        {
            // 버튼 이외의 곳을 응시했을 때 기존 버튼에 PointerExit 이벤트를 전달
            if (_prevButton != null)
            {
                ExecuteEvents.Execute(_prevButton, data, ExecuteEvents.pointerExitHandler);
                _prevButton.transform.GetChild(1).GetComponent<Image>().fillAmount = 0;
                _prevButton = null;
                _timePass = 0;
            }
        }
    }
}
