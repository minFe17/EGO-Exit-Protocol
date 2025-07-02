using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Dial : MonoBehaviour, ILoopObject
{
    [SerializeField] List<DialNumber> _dialNumberList = new List<DialNumber>();

    void Start()
    {
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    public void CheckTargetNumber()
    {
        for (int i = 0; i < _dialNumberList.Count; i++)
        {
            if (!_dialNumberList[i].IsEqualTargetNumber)
                return;
        }
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.CompleteDial);
        gameObject.SetActive(false);
    }

    #region Input System
    void OnClose()
    {
        gameObject.SetActive(false);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
    }
    #endregion

    void ILoopObject.OnLoopEvent()
    {
        gameObject.SetActive(false);
    }
}