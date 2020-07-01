using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextUI : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text _goods;
#pragma warning restore

    public void SetGoodsValue(double val)
    {
        if(val >= 10000)
        {   
            _goods.text = string.Format("{0:#,###}만", ((long)(val / 10000)));
        }
        else
        {
            _goods.text = string.Format("{0:#,###.##}만", val);
        }
    }
}
