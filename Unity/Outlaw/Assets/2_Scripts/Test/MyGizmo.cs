using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Color _color = Color.yellow;
    [SerializeField]
    float _radius = 0.5f;
#pragma warning restore

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
