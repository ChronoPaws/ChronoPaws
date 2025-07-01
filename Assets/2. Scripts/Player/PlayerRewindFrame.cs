using UnityEngine;

public struct PlayerRewindFrame
{
    public Vector3 position;
    public Vector3 scale;
    public int animationStateHash;
    public float animationNormalizedTime;

    public PlayerRewindFrame(Vector3 pos, Vector3 scl, int hash, float time)
    {
        position = pos;
        scale = scl;
        animationStateHash = hash;
        animationNormalizedTime = time;
    }
}
