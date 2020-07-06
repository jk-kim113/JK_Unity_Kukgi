using UnityEngine;

public struct MoveInfo
{
    public Vector3 _move;
    public Vector3 _dirStick;
    public bool _isAim;

    public MoveInfo(Vector3 move, Vector3 dirStick, bool isAim)
    {
        _move = move;
        _dirStick = dirStick;
        _isAim = isAim;
    }
}
