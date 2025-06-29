public interface IRewindable
{
    void Record();
    void Rewind();
    void StartRewind();
    void StopRewind();
}
