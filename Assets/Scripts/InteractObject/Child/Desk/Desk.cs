using UnityEngine;
using Utils;

public abstract class Desk : MonoBehaviour, IInteractable
{
    public abstract void InteractEvent();

    void Start()
    {
        Init();
    }

    protected virtual void Init()
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
        InteractEvent();
    }
    #endregion
}