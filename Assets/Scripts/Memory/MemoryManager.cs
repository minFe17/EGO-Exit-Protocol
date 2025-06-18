using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class MemoryManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // 싱글턴
    HashSet<EMemoryType> _newMemoryData = new HashSet<EMemoryType>();
    MemoryRepository _memoryRepository = new MemoryRepository();

    MediatorManager _mediatorManager;
    public MemoryRepository MemoryRepository { get => _memoryRepository; }

    public async Task Init()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.AddMemory, this);
        await _memoryRepository.Init();
    }

    void LoadNewMemory()
    {
        foreach(EMemoryType memoryType in _newMemoryData)
        {
            _memoryRepository.CurrentMemoryData.Add(memoryType);
            // UI에도 표시
        }
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        EMemoryType memoryType = (EMemoryType)data;
        if (_memoryRepository.CurrentMemoryData.Contains(memoryType))
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