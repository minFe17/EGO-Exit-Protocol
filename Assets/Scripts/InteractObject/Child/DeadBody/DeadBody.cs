using UnityEngine;
using Utils;

public class DeadBody : MonoBehaviour, IInteractable
{
    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        GenericSingleton<InteractObjectManager>.Instance.SetInteractable(gameObject, this);
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }

    public virtual void Interact()
    {

    }
}