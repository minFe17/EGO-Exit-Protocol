using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class TextUI : MonoBehaviour
{
    [SerializeField] protected Text _text;

    protected MediatorManager _mediatorManager;

    void Start()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        SetMediatorEvent();
    }

    protected abstract void SetMediatorEvent();
}