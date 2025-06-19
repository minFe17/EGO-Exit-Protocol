using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MemoryManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // ╫л╠шео
    HashSet<EMemoryType> _newMemoryData = new HashSet<EMemoryType>();
    MemoryRepository _memoryRepository = new MemoryRepository();

    MediatorManager _mediatorManager;
    public MemoryRepository MemoryRepository { get => _memoryRepository; }

    public void Init()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.AddMemory, this);
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        _memoryRepository.Init();
    }

    void LoadNewMemory()
    {
        foreach(EMemoryType memoryType in _newMemoryData)
        {
            MemoryPanelData newMemory = new MemoryPanelData(memoryType);
            _memoryRepository.CurrentMemoryData.Add(newMemory);
            _mediatorManager.Notify(EMediatorEventType.CreateMemoryPanel, newMemory);
        }
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        EMemoryType memoryType = (EMemoryType)data;
        if (_memoryRepository.ContainsMemoryType(memoryType))
            return;
        MemoryData memoryData = _memoryRepository.GetMemoryData(memoryType);
        _mediatorManager.Notify(EMediatorEventType.NeedCapture, memoryData);
        _newMemoryData.Add(memoryType);
    }

    void ILoopObject.OnLoopEvent()
    {
        LoadNewMemory();
        _newMemoryData.Clear();
    }
}