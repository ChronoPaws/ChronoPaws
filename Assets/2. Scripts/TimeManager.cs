using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private List<IRewindable> rewindables = new List<IRewindable>();
    private bool isRewinding = false;

    public bool IsRewinding => isRewinding;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartRewind();
        else if (Input.GetKeyUp(KeyCode.C))
            StopRewind();
    }

    void FixedUpdate()
    {
        foreach (IRewindable obj in rewindables)
        {
            if (isRewinding)
                obj.Rewind();
            else
                obj.Record();
        }
    }

    public void Register(IRewindable obj)
    {
        if (!rewindables.Contains(obj))
            rewindables.Add(obj);
    }

    public void Unregister(IRewindable obj)
    {
        rewindables.Remove(obj);
    }

    public void StartRewind()
    {
        isRewinding = true;
        foreach (IRewindable obj in rewindables)
            obj.StartRewind();
    }

    public void StopRewind()
    {
        isRewinding = false;
        foreach (IRewindable obj in rewindables)
            obj.StopRewind();
    }
}
