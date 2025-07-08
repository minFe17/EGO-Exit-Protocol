using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class EndingManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    EEndingType _endingType;

    public EEndingType EndingType { get => _endingType; }

    public void Init()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Ending, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _endingType = (EEndingType)data;
        GenericSingleton<MediatorManager>.Instance.ClearMediatorEvent();
        SceneManager.LoadScene("EndingScene");
    }
}