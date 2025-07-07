using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class EndingManager : MonoBehaviour, IMediatorEvent
{
    // 싱글턴
    EEndingType _endingType;

    public EEndingType EndingType { get => _endingType; }

    public void Init()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Ending, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _endingType = (EEndingType)data;
        // 페이드 연출?
        // 엔딩처리

        // 중재자 이벤트 다 삭제
        GenericSingleton<MediatorManager>.Instance.ClearMediatorEvent();
        SceneManager.LoadScene("EndingScene");
    }
}