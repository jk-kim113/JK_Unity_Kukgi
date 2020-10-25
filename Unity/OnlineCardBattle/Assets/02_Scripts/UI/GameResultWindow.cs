using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _gameResultTxt;
#pragma warning restore

    public void ShowGameReslut(string resultTxt)
    {
        _gameResultTxt.text = resultTxt;
    }
}
