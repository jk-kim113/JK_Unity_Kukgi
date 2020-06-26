using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _txtMapName;
#pragma warning restore

    public void InitSetData(string mapName)
    {
        _txtMapName.text = mapName;
    }
}
