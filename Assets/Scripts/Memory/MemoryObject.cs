using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MemoryObject : MonoBehaviour
{
    [SerializeField] List<EMemoryType> _memoryType;

    MediatorManager _mediatorManager;

    void Start()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    public void AddMemory()
    {
        for (int i = 0; i < _memoryType.Count; i++)
            _mediatorManager.Notify(EMediatorEventType.AddMemory, _memoryType[i]);
    }
}