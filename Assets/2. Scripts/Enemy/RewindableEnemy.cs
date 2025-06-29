using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(EnemyHealth))]
public class RewindableEnemy : MonoBehaviour, IRewindable
{
    public float recordTime = 3f;

    private List<RewindFrame> frames = new();
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyHealth healthScript;
    private float fixedDelta;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();
        fixedDelta = Time.fixedDeltaTime;

        TimeManager.Instance?.Register(this);
    }

    void OnDestroy()
    {
        TimeManager.Instance?.Unregister(this);
    }

    public void Record()
    {
        if (frames.Count > Mathf.Round(recordTime / fixedDelta))
            frames.RemoveAt(frames.Count - 1);

        var health = healthScript.GetHealth();
        var dead = healthScript.IsDead();

        frames.Insert(0, new RewindFrame(transform.position, transform.localScale, health, dead));
    }

    public void Rewind()
    {
        if (frames.Count > 0)
        {
            var frame = frames[0];

            transform.position = frame.position;
            transform.localScale = frame.scale;

            healthScript.SetHealth(frame.health);
            healthScript.SetDead(frame.isDead);

            frames.RemoveAt(0);
        }
    }

    public void StartRewind()
    {
        rb.isKinematic = true;
        anim.speed = 0;
    }

    public void StopRewind()
    {
        rb.isKinematic = false;
        anim.speed = 1;
    }
}
