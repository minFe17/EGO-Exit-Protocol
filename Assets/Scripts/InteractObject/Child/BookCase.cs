using NaughtyAttributes;
using UnityEngine;
using Utils;

public class BookCase : MonoBehaviour, IInteractable
{
    [SerializeField] bool _haveEvidence;

    [ShowIf("_haveEvidence")]
    [SerializeField] EvidenceObject _evidenceObject;

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
        if (_haveEvidence)
            _evidenceObject.AddEvidence();
    }
    #endregion
}