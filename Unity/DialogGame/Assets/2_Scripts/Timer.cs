using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    float _time;
#pragma warning restore

    private void OnEnable()
    {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(_time);

        gameObject.SetActive(false);
    }
}
