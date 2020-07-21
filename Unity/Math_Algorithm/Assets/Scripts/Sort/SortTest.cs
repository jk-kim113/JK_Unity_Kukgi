using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortTest : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text[] _numText;
    [SerializeField]
    Text _stateText;
#pragma warning restore

    static SortTest _uniqueInstance;
    public static SortTest _instance { get { return _uniqueInstance; } }

    int[] arr = new int[5];

    private void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        PrintState("배열 생성");
        for (int n = 0; n < arr.Length; n++)
        {
            arr[n] = Random.Range(0, 101);
            _numText[n].text = arr[n].ToString();
        }

        //StartCoroutine(SortClass.SeletedSort(arr));
        //StartCoroutine(SortClass.InsertSort(arr, true));
        StartCoroutine(SortClass.BubbleSort(arr));
    }

    public void PrintArray()
    {
        for (int n = 0; n < arr.Length; n++)
        {   
            _numText[n].text = arr[n].ToString();
        }
    }

    public void PrintState(string str)
    {
        _stateText.text = str;
    }
}
