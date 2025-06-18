using UnityEngine;
using Utils;

public class Rope : MonoBehaviour, IInteractable
{
    [SerializeField] EMemoryType _memoryType;

    InteractObjectManager _interactObjectManager;
    MediatorManager _mediatorManager;
    MemoryManager _memoryManager;
    
    MemoryData _memoryData;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _memoryManager = GenericSingleton<MemoryManager>.Instance;
        _memoryData = _memoryManager.MemoryRepository.GetMemoryData(_memoryType);
        Debug.Log(2);
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
}