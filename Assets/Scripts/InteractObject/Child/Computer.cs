using NaughtyAttributes;
using UnityEngine;
using Utils;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] bool _haveEvidence;
    [SerializeField] bool _haveMemory;

    [ShowIf("_haveEvidence")]
    [SerializeField] EvidenceObject _evidenceObject;

    [ShowIf("_haveMemory")]
    [SerializeField] MemoryObject _memoryObject;

    void Start()
    {
        Init();
    }

    void Init()
    {
        GenericSingleton<InteractObjectManager>.Instance.SetInteractable(gameObject, this);
    }

    #region Interface
    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }

    void IInteractable.Interact()
    {
        if (DataSingleton<LoopData>.Instance.LoopCount < 2)
            return;
        if (_haveEvidence)
            _evidenceObject.AddEvidence();
        if(_haveMemory)
            _memoryObject.AddMemory();
    }
    #endregion
}