using UnityEngine;
using Utils;

public class PhoneEvent : IMediatorEvent
{
    GameObject _phonePanel;

    public void Init(GameObject phonePanel)
    {
        _phonePanel = phonePanel;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.UsePhone, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _phonePanel.SetActive(true);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimePause);
    }
}