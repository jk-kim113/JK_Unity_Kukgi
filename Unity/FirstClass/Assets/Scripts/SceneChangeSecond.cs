using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSecond : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(435, 330, 300, 80), "Scene Change"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
