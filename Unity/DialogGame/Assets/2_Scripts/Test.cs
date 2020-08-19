using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    Image _image;
    [SerializeField]
    Text _text;

    int _index = 1;

    private void Start()
    {
        _index = 1;
        TableManager._instance.LoadAll();
    }

    public void ClickBtn()
    {
        _image.sprite = ResourcePoolManager._instance.GetImage(
                            TableManager._instance.Get(eTableType.Dialog).ToS(_index, "ImageName"));

        _text.text = TableManager._instance.Get(eTableType.Dialog).ToS(_index++, "Sentences");
    }
}
