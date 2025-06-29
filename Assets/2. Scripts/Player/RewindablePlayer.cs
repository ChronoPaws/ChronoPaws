using System.Collections.Generic;
using UnityEngine;

public class RewindablePlayer : MonoBehaviour, IRewindable
{
    public float recordTime = 3f;

    private List<PlayerRewindFrame> frames = new();
    private Rigidbody2D rb;
    private Animator anim;
    private float fixedDelta;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        frames.Insert(0, new PlayerRewindFrame(transform.position, transform.localScale));
    }

    public void Rewind()
    {
        if (frames.Count > 0)
        {
            var frame = frames[0];
            transform.position = frame.position;
            transform.localScale = frame.scale;
            frames.RemoveAt(0);
        }
    }

    public void StartRewind()
    {
        if (rb != null)
            rb.isKinematic = true;

        if (anim != null)
            anim.SetBool("Rewinding", true); // Si tienes esta animación opcional
    }

    public void StopRewind()
    {
        if (rb != null)
            rb.isKinematic = false;

        if (anim != null)
            anim.SetBool("Rewinding", false);
    }
}
