using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    const float _stdGravity = 9.81f;

    private void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width / 2) - 75, 0, 150, 40), "↑"))
        {
            Physics.gravity = new Vector3(0, 0, _stdGravity);
        }
        else if (GUI.Button(new Rect((Screen.width / 2) - 75, Screen.height - 40, 150, 40), "↓"))
        {
            Physics.gravity = new Vector3(0, 0,-_stdGravity);
        }
        else if (GUI.Button(new Rect(0, Screen.height / 2 - 75, 40, 150), "←"))
        {
            Physics.gravity = new Vector3(-_stdGravity, 0, 0);
        }
        else if (GUI.Button(new Rect(Screen.width - 40, Screen.height / 2 - 75, 40, 150), "→"))
        {
            Physics.gravity = new Vector3(_stdGravity, 0, 0);
        }
    }
}
