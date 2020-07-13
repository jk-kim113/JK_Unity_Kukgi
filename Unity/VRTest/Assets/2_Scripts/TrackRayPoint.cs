using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRayPoint : MonoBehaviour
{
    [SerializeField]
    Color _lineColor = Color.yellow;
    Transform[] _points;

    private void OnDrawGizmos()
    {
        Gizmos.color = _lineColor;
        _points = GetComponentsInChildren<Transform>();
        int idx = 1;

        Vector3 currPos = _points[idx].position;
        Vector3 nextPos;

        for(int n = 0; n <= _points.Length; n++)
        {
            nextPos = (++idx >= _points.Length) ? _points[1].position : _points[idx].position;
            Gizmos.DrawLine(currPos, nextPos);
            currPos = nextPos;
        }
    }
}
