using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IGvrPointerHoverHandler
{
    public void OnLookItemBox(bool isLookAt)
    {
        Debug.Log(isLookAt);
        PlayerControl._isStop = isLookAt;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerControl._isStop = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerControl._isStop = false;
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering!!");
    }
}
