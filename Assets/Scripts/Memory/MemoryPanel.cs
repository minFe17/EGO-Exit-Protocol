using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

/// <summary>
/// 기억 조각 UI를 관리하는 클래스
/// </summary>
public class MemoryPanel : MonoBehaviour, IMemento
{
    [SerializeField] Image _image;
    [SerializeField] Text _description;

    Stack<MemoryMemento> _mementoStack = new Stack<MemoryMemento>();
    MemoryPanelData _memoryPanelData;
    MemoryManager _memoryManager;
    MemoryData _memoryData;
    BoardUI _board;
    RectTransform _rectTransform;

    Vector2 _lastMousePosition;
    Vector2 _halfSize;
    Vector2 _minBoundSize;
    Vector2 _maxBoundSize;
    Rect _rect;

    public void Init(MemoryPanelData memoryPanelData, BoardUI parent, float yBound)
    {
        _board = parent;
        _rect = _board.GetComponent<RectTransform>().rect;
        _rectTransform = GetComponent<RectTransform>();
        _memoryPanelData = memoryPanelData;
        _memoryManager = GenericSingleton<MemoryManager>.Instance;
        _memoryData = _memoryManager.MemoryRepository.GetMemoryData(_memoryPanelData.MemoryType);
        ShowMemory();
        CalculateBound(yBound);
    }

    void ShowMemory()
    {
        _rectTransform.anchoredPosition = _memoryPanelData.Position.Value;
        _image.sprite = _memoryData.Sprite;
        _description.text = _memoryData.Description;
    }

    /// <summary>
    /// 패널의 이동 제한 범의 계산
    /// </summary>
    /// <param name="yBound">y축 최대 이동 제한 값</param>
    void CalculateBound(float yBound)
    {
        _halfSize = _rectTransform.rect.size * _rectTransform.pivot;
        _minBoundSize = new Vector2(_rect.min.x + _halfSize.x, -yBound);
        _maxBoundSize = new Vector2(_rect.max.x - _halfSize.x, yBound);
    }

    #region Event Trigger
    /// <summary>
    /// 드래그 시작 시 호출
    /// 현재 의치 저장(되돌리기 기능 의해)
    /// </summary>
    public void BeginDrag(BaseEventData data)
    {
        Save();
        PointerEventData eventData = (PointerEventData)data;
        _lastMousePosition = eventData.position;
    }

    /// <summary>
    /// 드래그 중 호출
    /// 마우스 이동에 따라 기억 조각 위치를 이동시키고, 이동 범위를 제한
    /// </summary>
    public void Drag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        Vector2 movePos = eventData.position - _lastMousePosition;

        Vector2 pos = _rectTransform.anchoredPosition + movePos;
        float clampX = Mathf.Clamp(pos.x, _minBoundSize.x, _maxBoundSize.x);
        float clampY = Mathf.Clamp(pos.y, _minBoundSize.y, _maxBoundSize.y);

        Vector2 newPos = new Vector2(clampX, clampY);

        _rectTransform.anchoredPosition = newPos;
        _memoryPanelData.Position = newPos; 
        _lastMousePosition = eventData.position;
    }

    /// <summary>
    /// 드래그 종료 시 호출
    /// 현재 상태를 Json 파일로 저장
    /// </summary>
    public void EndDrag(BaseEventData data)
    {
        GenericSingleton<JsonManager>.Instance.WriteData.WriteCurrentMemoryData();
    }
    #endregion

    #region Interface
    /// <summary>
    /// 현재 위치 상태를 저장
    /// </summary>
    public void Save()
    {
        _mementoStack.Push(new MemoryMemento(_rectTransform.anchoredPosition));
        _board.Save(this);
    }

    /// <summary>
    /// 이전 상태로 위치 복원(되돌리기)
    /// </summary>
    void IMemento.Restore()
    {
        if(_mementoStack.Count > 0 )
        {
            MemoryMemento memento = _mementoStack.Pop();
            _rectTransform.anchoredPosition = memento.Position;
        }
    }
    #endregion
}