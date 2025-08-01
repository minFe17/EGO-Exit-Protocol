using System.Collections.Generic;

/// <summary>
/// 루프 이벤트를 관리하는 클래스
/// ILoopObject를 구현한 객체들을 리스트에 등록
/// </summary>
public class LoopObserve
{
    // 루프 이벤트를 등록한 ILoopObject 리스트
    private List<ILoopObject> _loopEvents = new List<ILoopObject>();

    /// <summary>
    /// 루프 이벤트 객체를 리스트에 추가
    /// 이미 등록된 객체는 중복 추가 X
    /// </summary>
    public void AddLoopEvent(ILoopObject loopEvent)
    {
        // 이미 리스트에 존재하는 경우 추가 X
        if (_loopEvents.Contains(loopEvent))
            return;

        // 리스트에 객체 추가
        _loopEvents.Add(loopEvent);
    }

    /// <summary>
    /// 등록된 모든 루프 이벤트 객체의 OnLoopEvent 메서드를 호출
    /// 이 메서드는 루프가 돌 때마다 호출되어야 함
    /// </summary>
    public void OnLoopEvent()
    {
        // 리스트에 등록된 각 객체의 OnLoopEvent 메서드 호출
        for (int i = 0; i < _loopEvents.Count; i++)
            _loopEvents[i].OnLoopEvent();
    }

    /// <summary>
    /// 루프 이벤트 객체를 리스트에서 제거
    /// </summary>
    public void RemoveLoopEvent(ILoopObject loopEvent)
    {
        // 리스트에 없는 경우 제거 X
        if (!_loopEvents.Contains(loopEvent))
            return;

        // 리스트에서 객체 제거
        _loopEvents.Remove(loopEvent);
    }
}