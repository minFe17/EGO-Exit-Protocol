using UnityEngine;
using Utils;

public class TimeEventBase : MonoBehaviour
{
    protected TimeManager _timeManager;

    void Start()
    {
        _timeManager = GenericSingleton<TimeManager>.Instance;
    }
}
