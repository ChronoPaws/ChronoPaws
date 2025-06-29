using UnityEngine;

[System.Serializable]
public struct PlayerRewindFrame
{
    public Vector3 position;
    public Vector3 scale;

    public PlayerRewindFrame(Vector3 pos, Vector3 scl)
    {
        position = pos;
        scale = scl;
    }
}
