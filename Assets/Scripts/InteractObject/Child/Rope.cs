using UnityEngine;
using Utils;

public class Rope : MonoBehaviour, IInteractable, ILoopObject
{
    [SerializeField] EMemoryType _memoryType;

    InteractObjectManager _interactObjectManager;
    MediatorManager _mediatorManager;
    MementoManager _mementoManager;
    MemoryManager _memoryManager;
    
    MemoryData _memoryData;

    void Start()
    {
        Init();
        OnLoopEvent();
    }

    void Init()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mementoManager= GenericSingleton<MementoManager>.Instance;
        _memoryManager = GenericSingleton<MemoryManager>.Instance;
        _memoryData = _memoryManager.MemoryRepository.GetMemoryData(_memoryType);
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }

    void IInteractable.Interact()
    {
        // 줄 떨어지는 연출
        _mediatorManager.Notify(EMediatorEventType.NeedCapture, _memoryData);
        _mediatorManager.Notify(EMediatorEventType.RopeReleased);
    }

    public void OnLoopEvent()
    {
        transform.position = _mementoManager.RopeMemento.Position;
    }
}