using UnityEngine;

public class DoorMemento : MonoBehaviour
{
    [SerializeField] bool _isLock;
    [SerializeField] bool _isTrigger;

    public bool IsLock { get => _isLock; }
    public bool IsTrigger { get => _isTrigger; }
}