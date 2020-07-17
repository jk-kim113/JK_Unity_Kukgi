using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    Animation _anim;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
    }

    private void Start()
    {
        _anim.Play("ScoreUIEffect");
        Destroy(gameObject, 2.0f);
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
