using UnityEngine;
using NaughtyAttributes;

public class DoorMemento : MonoBehaviour
{
    [ShowIf("_isLock")]
    [SerializeField] EItemType _needUnlockItem;
    [SerializeField] bool _isLock;
    [SerializeField] bool _isTrigger;

    public bool IsLock { get => _isLock; }
    public bool IsTrigger { get => _isTrigger; }
    public EItemType NeedUnlockItem { get => _needUnlockItem; }
}