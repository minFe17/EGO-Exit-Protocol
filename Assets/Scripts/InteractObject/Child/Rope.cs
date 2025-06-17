using UnityEngine;
using Utils;

public class Rope : MonoBehaviour, IInteractable
{
    InteractObjectManager _interactObjectManager;
    MediatorManager _mediatorManager;

    void Start()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }

    void IInteractable.Interact()
    {
        // �� �������� ����
        _mediatorManager.Notify(EMediatorEventType.RopeReleased);
    }
}