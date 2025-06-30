using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class BoardUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float _borderBoundary;
    [SerializeField] float _xOffset;
    [SerializeField] float _yBoundary;

    Stack<IMemoryMemento> _memoryPanelStack = new Stack<IMemoryMemento>();
    MediatorManager _mediatorManager;
    PrefabLoadBase _uIPrefabLoad;
    Vector2 _lastMousePosition;

    public void Init(EMediatorEventType eventType)
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(eventType, this);
        _uIPrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.UI);
    }

    Vector2 RandomPosition()
    {
        float screenWidth = Screen.width;
       
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

       
        Vector2 minScreenPosOfRect = RectTransformUtility.WorldToScreenPoint(null, corners[0]); // 좌하단
        Vector2 maxScreenPosOfRect = RectTransformUtility.WorldToScreenPoint(null, corners[2]); // 우상단

        
        float effectiveMinX = Mathf.Max(minScreenPosOfRect.x, 0);
        float effectiveMaxX = Mathf.Min(maxScreenPosOfRect.x, screenWidth);

        effectiveMinX += _xOffset;
        effectiveMaxX -= _xOffset;

        float randomX = Random.Range(effectiveMinX, effectiveMaxX);
        float randomY = Random.Range(-_yBoundary, _yBoundary);

        Vector2 randomPos = new Vector2(randomX, randomY);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, randomPos, null, out Vector2 position);

        return position;
    }

    #region Event Trigger
    public void BeginDrag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        _lastMousePosition = eventData.position;
    }

    public void Drag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        Vector2 movePos = eventData.position - _lastMousePosition;

        Vector2 newPos = rectTransform.anchoredPosition + new Vector2(movePos.x, 0);

        if (newPos.x > _borderBoundary)
            newPos.x = _borderBoundary;
        else if (newPos.x < -_borderBoundary)
            newPos.x = -_borderBoundary;

        rectTransform.anchoredPosition = newPos;
        _lastMousePosition = eventData.position;
    }
    #endregion

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        MemoryPanelData memoryPanelData = (MemoryPanelData)data;
        if (memoryPanelData.Position == null)
            memoryPanelData.Position = RandomPosition();

        GameObject temp = Instantiate(_uIPrefabLoad.GetPrefab(EUIPrefabType.MemoryPanel), this.gameObject.transform);
        temp.GetComponent<MemoryPanel>().Init(memoryPanelData, this, _yBoundary);
        GenericSingleton<JsonManager>.Instance.WriteData.WriteCurrentMemoryData();
    }

    public void Save(IMemoryMemento memoryMemento)
    {
        _memoryPanelStack.Push(memoryMemento);
        // 파일 쓰기
    }

    public void Restore()
    {
        if (_memoryPanelStack.Count > 0)
        {
            IMemoryMemento memory = _memoryPanelStack.Pop();
            memory.Restore();
            // 파일쓰기?
        }
    }
    #endregion;
}