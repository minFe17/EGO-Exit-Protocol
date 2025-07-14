using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 기억 시스템을 관리하는 클래스
/// MemoryData를 기반으로 UI에 보여줄 MemoryPanel 생성 요청
/// 기억조각 중복 등록 방지
/// </summary>
public class MemoryManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // 싱글턴
    HashSet<EMemoryType> _newMemoryData = new HashSet<EMemoryType>();
    MemoryRepository _memoryRepository = new MemoryRepository();

    MediatorManager _mediatorManager;
    public MemoryRepository MemoryRepository { get => _memoryRepository; }

    public void Init()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.AddMemory, this);
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        _memoryRepository.Init();
    }

    /// <summary>
    /// 저장된 신규 기억조각 타입 목록을 기반으로 MemoryPanelData를 생성하고 UI 생성을 요청
    /// </summary>
    void LoadNewMemory()
    {
        foreach(EMemoryType memoryType in _newMemoryData)
        {
            MemoryPanelData newMemory = new MemoryPanelData(memoryType);

            // 현재 보유한 기억조각 리스트에 추가
            DataSingleton<CurrentMemoryList>.Instance.CurrtenMemoryData.Add(newMemory);

            // MemoryPanel 생성 요청
            _mediatorManager.Notify(EMediatorEventType.CreateMemoryPanel, newMemory);
        }
    }

    /// <summary>
    /// 중재자 이벤트 수신 처리
    /// 이미 존재하는 기억 타입이면 무시
    /// 캡처가 필요한 경우 NeedCapture 이벤트를 요청하고 대기 목록에 추가
    /// </summary>
    void IMediatorEvent.HandleEvent(object data)
    {
        EMemoryType memoryType = (EMemoryType)data;

        // 이미 등록된 기억은 return
        if (_memoryRepository.ContainsMemoryType(memoryType))
            return;

        MemoryData memoryData = _memoryRepository.GetMemoryData(memoryType);

        // 캡처 요청
        _mediatorManager.Notify(EMediatorEventType.NeedCapture, memoryData);
        _newMemoryData.Add(memoryType);
    }

    void ILoopObject.OnLoopEvent()
    {
        LoadNewMemory();
        _newMemoryData.Clear();
    }
}