using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour
{
    EventTrigger _trigger;

    private void Awake()
    {
        _trigger = gameObject.AddComponent<EventTrigger>();

        // 이벤트를 정의한다.
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        // 마우스 커서 또는 Raycast가 hover 됐을 때 발생 하도록 한다.
        entry1.eventID = EventTriggerType.PointerEnter;
        // 이벤트 발생시의 호출될 함수를 연결.
        entry1.callback.AddListener(delegate { HoverButton(true); });
        // 만든 이벤트를 이벤트 트리거에 추가한다.
        _trigger.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener(delegate { HoverButton(false); });
        _trigger.triggers.Add(entry2);

        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerClick;
        entry3.callback.AddListener(delegate { ClickButton(gameObject.name); });
        _trigger.triggers.Add(entry3);
    }

    void HoverButton(bool isHover)
    {
        Debug.Log("Button Statement : " + isHover);
    }

    void ClickButton(string btnName)
    {
        Debug.Log("Click : " + btnName);
    }
}
