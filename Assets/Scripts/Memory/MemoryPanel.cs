using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public class MemoryPanel : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image _image;
    [SerializeField] Text _description;

    MemoryPanelData _memoryPanelData;
    MemoryManager _memoryManager;
    MemoryData _memoryData;
    Vector2 _lastMousePosition;
    Vector2 _halfSize;
    Vector2 _minBoundSize;
    Vector2 _maxBoundSize;
    Rect _board;

    public void Init(MemoryPanelData memoryPanelData, RectTransform parent, float yBound)
    {
        _board = parent.rect;
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
        _minBoundSize = new Vector2(_board.min.x + _halfSize.x, -yBound);
        _maxBoundSize = new Vector2(_board.max.x - _halfSize.x, yBound);
    }

    public void BeginDrag(BaseEventData data)
    {
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
}