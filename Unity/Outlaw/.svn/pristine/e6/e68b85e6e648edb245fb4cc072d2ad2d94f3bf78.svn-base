using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StickObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
#pragma warning disable 0649
    [SerializeField]
    Color _DownColor;
#pragma warning restore
    Color _oriColor;

    bool _isAim = false;
    public bool _isAimMotion { get { return _isAim; } }

    Image _bg;
    Image _stick;
    Vector3 _dirInput;
    public Vector3 _direction { get { return _dirInput.normalized; } }
    public Vector3 _dirMov { get { return (_dirInput.magnitude > 1.0f) ? _dirInput.normalized : _dirInput; } }

    Player _ownerPlayer;

    private void Start()
    {
        _bg = GetComponent<Image>();
        _stick = transform.GetChild(0).GetComponent<Image>();
        _oriColor = _stick.color;
    }

    public void SetOwnerPlayer(Player p)
    {
        _ownerPlayer = p;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_bg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _bg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _bg.rectTransform.sizeDelta.y);

            _dirInput = new Vector3(pos.y, 0, -pos.x);
            Vector3 dir = (pos.magnitude > 1.0f) ? pos.normalized : pos;

            _stick.rectTransform.anchoredPosition = new Vector3(dir.x * (_bg.rectTransform.sizeDelta.x / 3), dir.y * (_bg.rectTransform.sizeDelta.y/ 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isAim = true;
        _stick.color = _DownColor;
        _ownerPlayer.InitializeDirection();

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isAim = false;
        _stick.color = _oriColor;
        
        _dirInput = Vector3.zero;
        _stick.rectTransform.anchoredPosition = Vector3.zero;
    }
}
