using UnityEngine;

public struct PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public int nowAct;

    public PointInTime(Vector3 _position, Quaternion _rotation, int _nowAct)
    {
        position = _position;
        rotation = _rotation;
        nowAct = _nowAct;
    }
}
