using UnityEngine;
using Utils;

public class DialEvent : IMediatorEvent
{
    GameObject _dialPanel;
    public void Init(GameObject dialPanel)
    {
        _dialPanel = dialPanel;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.UseDial, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _dialPanel.SetActive(true);
        Time.timeScale = 0;
    }
}