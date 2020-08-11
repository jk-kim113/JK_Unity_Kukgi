using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSlot : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _nonSelectImg;
#pragma warning restore

    public void OnOffSlot(bool isOn)
    {
        _nonSelectImg.SetActive(!isOn);
    }
}
