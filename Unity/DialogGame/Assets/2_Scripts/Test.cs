using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    Text _textBox;

    int _diIndex = 1;

    public void ClickButton()
    {
        _textBox.text = JsonManager._instance.GetTable("Dialog").GetToStr(_diIndex++, "Sentences");
        //Debug.Log(JsonManager._instance.GetTable("Scenario").GetToInt(_diIndex++, "StartIndex"));
    }
}
