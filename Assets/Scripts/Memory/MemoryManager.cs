using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MemoryManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    HashSet<EMemoryType> _newMemoryData = new HashSet<EMemoryType>();
    MemoryRepository _memoryRepository = new MemoryRepository();

    MediatorManager _mediatorManager;

    public void Init()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.AddMemory, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        EMemoryType memoryType = (EMemoryType)data;
        if (_memoryRepository.CurrentMemoryData.Contains(memoryType))
            return;
        _newMemoryData.Add(memoryType);
        MemoryData memoryData = _memoryRepository.GetMemoryData(memoryType);
        _mediatorManager.Notify(EMediatorEventType.NeedCapture, memoryData);
    }
}