using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject[] _gateSideObjs;
    [SerializeField]
    GameObject _menuWnd;
    [SerializeField]
    GameObject _okButton;
    [SerializeField]
    float _damping = 3.0f;
#pragma warning restore

    Animation _anim;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
    }

    private void Start()
    {
        _menuWnd.SetActive(false);
        _okButton.SetActive(false);
    }

    public void OnOffMenuWindow(bool isOpen)
    {
        _menuWnd.SetActive(isOpen);
    }

    public void OpenGate()
    {
        _anim.Play("GateOpen");
    }

    public void OnOffOkbuton(bool isOpen)
    {
        _okButton.SetActive(isOpen);
    }
}
