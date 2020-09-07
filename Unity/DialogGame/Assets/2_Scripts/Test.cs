using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Image _image;
    [SerializeField]
    Text _text;
#pragma warning restore

    int _index = 1;

    private void Start()
    {
        _index = 1;
        TableManager._instance.LoadAll();
        ResourcePoolManager._instance.ResourceAllLoad();
    }

    public void ClickBtn()
    {
        _image.sprite = ResourcePoolManager._instance.GetObj<Sprite>(ResourcePoolManager.eResourceKind.Image,
                            TableManager._instance.Get(eTableType.Dialog).ToS(_index, "ImageName"));
        _image.SetNativeSize();
        _text.text = TableManager._instance.Get(eTableType.Dialog).ToS(_index++, "Sentences");
    }
}
