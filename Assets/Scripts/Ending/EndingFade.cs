using UnityEngine;
using Utils;

public class EndingFade : MonoBehaviour, IMediatorEvent
{
    Animator _animator;
    EEndingType _endingType;
    void Start()
    {
        _animator = GetComponent<Animator>();
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.StartEndingFade, this);
    }

    #region Animation Event
    void EndFade()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.Ending, _endingType);
    }
    #endregion

    void IMediatorEvent.HandleEvent(object data)
    {
        _endingType = (EEndingType)data;
        _animator.SetBool("isEnding", true);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimePause);
    }
}