using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public GameObject _prefabMagic;
    public float _timeStandard = 1.0f;
    float _timeSave = 0;

    private void Update()
    {
        _timeSave += Time.deltaTime;
        if(_timeSave >= _timeStandard)
        {
            _timeSave = 0;
            Instantiate(_prefabMagic, transform.position, transform.rotation);
        }
    }
}
