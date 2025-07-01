using NaughtyAttributes;
using UnityEngine;
using Utils;

public class Cabinet : MonoBehaviour, IInteractable
{
    [SerializeField] MemoryObject _memoryObject;
    [SerializeField] bool _isTrap;
    [ShowIf("_isTrap")]
    [SerializeField] Vector3 _researcherSpawnPosition;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        GenericSingleton<InteractObjectManager>.Instance.SetInteractable(gameObject, this);
    }

    #region NaughtyAttributes
    bool IsNotTrap() => !_isTrap;
    #endregion

    #region Interface
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public virtual void Interact()
    {
        if (_isTrap)
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPosition);
        else
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.UseDial);
        if (_memoryObject != null)
            _memoryObject.AddMemory();
    }
    #endregion
}