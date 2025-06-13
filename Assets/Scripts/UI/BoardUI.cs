using UnityEngine;
using UnityEngine.EventSystems;

public class BoardUI : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float _borderBoundary;
    
    Vector2 lastMousePosition;

    public void BeginDrag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        lastMousePosition = eventData.position;
    }

    public void Drag(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        Vector2 movePos = eventData.position - lastMousePosition;

        Vector2 newPos = rectTransform.anchoredPosition + new Vector2(movePos.x, 0);

        if (newPos.x > _borderBoundary)
            newPos.x = _borderBoundary;
        else if (newPos.x < -_borderBoundary)
            newPos.x = -_borderBoundary;

        rectTransform.anchoredPosition = newPos;
        lastMousePosition = eventData.position;
    }
}