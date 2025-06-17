using UnityEngine;
using Utils;

public class ExitPlayer : MonoBehaviour, IMediatorEvent
{
    TiedUpState _tiedUpState;
    MediatorManager _mediatorManager;

    public void Init(TiedUpState tiedUpState)
    {
        _tiedUpState = tiedUpState;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.PlayerExitAssistantRoom, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _tiedUpState.ExitPlayer();
    }
}