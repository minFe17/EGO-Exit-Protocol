using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public class MemoryPanel : MonoBehaviour, IMemento
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image _image;
    [SerializeField] Text _description;

    Stack<MemoryMemento> _mementoStack = new Stack<MemoryMemento>();
    MemoryPanelData _memoryPanelData;
    MemoryManager _memoryManager;
    MemoryData _memoryData;
    BoardUI _board;

    Vector2 _lastMousePosition;
    Vector2 _halfSize;
    Vector2 _minBoundSize;
    Vector2 _maxBoundSize;
    Rect _rect;

    public void Init(MemoryPanelData memoryPanelData, BoardUI parent, float yBound)
    {
        _board = parent;
        _rect = _board.GetComponent<RectTransform>().rect;
        _memoryPanelData = memoryPanelData;
        _memoryManager = GenericSingleton<MemoryManager>.Instance;
        _memoryData = _memoryManager.MemoryRepository.GetMemoryData(_memoryPanelData.MemoryType);
        ShowMemory();
        CalculateBound(yBound);
    }

    void ShowMemory()
    {
        rectTransform.anchoredPosition = _memoryPanelData.Position.Value;
        _image.sprite = _memoryData.Sprite;
        _description.text = _memoryData.Description;
    }

    void CalculateBound(float yBound)
    {
        _halfSize = rectTransform.rect.size * rectTransform.pivot;
        _minBoundSize = new Vector2(_rect.min.x + _halfSize.x, -yBound);
        _maxBoundSize = new Vector2(_rect.max.x - _halfSize.x, yBound);
    }

    #region Event Trigger
    public void BeginDrag(BaseEventData data)
    {
        Save();
        PointerEventData eventData = (PointerEventData)data;
        _lastMousePosition = eventData.position;
    }

    public void Drag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        Vector2 movePos = eventData.position - _lastMousePosition;

        Vector2 pos = rectTransform.anchoredPosition + movePos;
        float clampX = Mathf.Clamp(pos.x, _minBoundSize.x, _maxBoundSize.x);
        float clampY = Mathf.Clamp(pos.y, _minBoundSize.y, _maxBoundSize.y);

        Vector2 newPos = new Vector2(clampX, clampY);

        rectTransform.anchoredPosition = newPos;
        _memoryPanelData.Position = newPos; 
        _lastMousePosition = eventData.position;
    }

    public void EndDrag(BaseEventData data)
    {
        GenericSingleton<JsonManager>.Instance.WriteData.WriteCurrentMemoryData();
    }
    #endregion

    #region Interface
    public void Save()
    {
        _mementoStack.Push(new MemoryMemento(rectTransform.anchoredPosition));
        _board.Save(this);
    }

    void IMemento.Restore()
    {
        if(_mementoStack.Count > 0 )
        {
            MemoryMemento memento = _mementoStack.Pop();
            rectTransform.anchoredPosition = memento.Position;
        }
    }
    #endregion
}