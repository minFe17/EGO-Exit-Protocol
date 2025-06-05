using UnityEngine;
using Utils;

public class Box : MonoBehaviour, IInteractable
{
    InteractObjectManager _interactObjectManager;

    void Start()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
    }

    void IInteractable.Interact()
    {
        Debug.Log(1);
    }
}