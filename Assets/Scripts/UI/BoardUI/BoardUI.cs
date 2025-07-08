using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public abstract class BoardUI : MonoBehaviour
{
    [SerializeField] float _borderBoundary;
    [SerializeField] float _xOffset;
    [SerializeField] protected float _yBoundary;

    RectTransform _rectTransform;
    Vector2 _lastMousePosition;

    protected MediatorManager _mediatorManager;
    public abstract void Save(IMemento memento);
    public abstract void Restore();

    public virtual void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    protected Vector2 RandomPosition()
    {
        float screenWidth = Screen.width;
       
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);

       
        Vector2 minScreenPosOfRect = RectTransformUtility.WorldToScreenPoint(null, corners[0]); // 좌하단
        Vector2 maxScreenPosOfRect = RectTransformUtility.WorldToScreenPoint(null, corners[2]); // 우상단

        
        float effectiveMinX = Mathf.Max(minScreenPosOfRect.x, 0);
        float effectiveMaxX = Mathf.Min(maxScreenPosOfRect.x, screenWidth);

        effectiveMinX += _xOffset;
        effectiveMaxX -= _xOffset;

        float randomX = Random.Range(effectiveMinX, effectiveMaxX);
        float randomY = Random.Range(-_yBoundary, _yBoundary);

        Vector2 randomPos = new Vector2(randomX, randomY);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, randomPos, null, out Vector2 position);

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

        Vector2 newPos = _rectTransform.anchoredPosition + new Vector2(movePos.x, 0);

        if (newPos.x > _borderBoundary)
            newPos.x = _borderBoundary;
        else if (newPos.x < -_borderBoundary)
            newPos.x = -_borderBoundary;

        _rectTransform.anchoredPosition = newPos;
        _lastMousePosition = eventData.position;
    }
    #endregion
}