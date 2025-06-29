using UnityEngine;

[System.Serializable]
public struct RewindFrame
{
    public Vector3 position;
    public Vector3 scale;
    public float health;
    public bool isDead;

    public RewindFrame(Vector3 pos, Vector3 scl, float hp, bool dead)
    {
        position = pos;
        scale = scl;
        health = hp;
        isDead = dead;
    }
}
