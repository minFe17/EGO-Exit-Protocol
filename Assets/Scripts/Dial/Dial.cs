using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Dial : MonoBehaviour
{
    [SerializeField] List<DialNumber> _dialNumberList = new List<DialNumber>();

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
}