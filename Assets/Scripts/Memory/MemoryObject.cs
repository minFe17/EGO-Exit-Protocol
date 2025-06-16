using UnityEngine;
using Utils;

public class MemoryObject : MonoBehaviour
{
    [SerializeField] EMemoryType _memoryType;

    MediatorManager _mediatorManager;

    void Start()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    public void AddMemory()
    {
        _mediatorManager.Notify(EMediatorEventType.AddMemory, _memoryType);
    }
}