using UnityEngine;
using Utils;

public class EnterPlayer : MonoBehaviour, IMediatorEvent
{
    TiedUpState _tiedUpState;
    MediatorManager _mediatorManager;

    public void Init(TiedUpState tiedUpState)
    {
        Debug.Log("EnterInit");

        _tiedUpState = tiedUpState;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.PlayerEnterAssistantRoom, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        Debug.Log("Enter");
        _tiedUpState.EnterPlayer();
    }
}