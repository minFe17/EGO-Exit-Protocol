public interface IDoorEvent
{
    void OnUnlock();
    void OnUnlockFail();
    void OnInteract();
}