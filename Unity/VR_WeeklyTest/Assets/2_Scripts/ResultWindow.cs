using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _killCount;
    [SerializeField]
    Text _deathCount;
#pragma warning restore

    public void OpenWnd(int kill, int death)
    {
        _killCount.text = kill.ToString();
        _deathCount.text = death.ToString();
    }
}
