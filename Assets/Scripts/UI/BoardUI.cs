using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class BoardUI : MonoBehaviour, IMediatorEvent
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float _borderBoundary;
    [SerializeField] float _yBoundary = 80f;

    MediatorManager _mediatorManager;
    PrefabLoadBase _uIPrefabLoad;
    Vector2 lastMousePosition;

    public void Init(EMediatorEventType eventType)
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(eventType, this);
        _uIPrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.UI);
    }

    Vector3 RandomPosition()
    {
        float visibleLeft = rectTransform.offsetMin.x;
        float visibleRight = rectTransform.offsetMax.x;

        float min = Mathf.Min(visibleLeft, visibleRight);
        float max = Mathf.Max(visibleLeft, visibleRight);

        float randomX = Random.Range(min, max);
        float randomY = Random.Range(-_yBoundary, _yBoundary);

        return new Vector3(randomX, randomY);
    }

    #region Event Trigger
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
    #endregion

    void IMediatorEvent.HandleEvent(object data)
    {
        Debug.Log(2);
        MemoryPanelData memoryPanelData = (MemoryPanelData)data;
        if (memoryPanelData.Position == null)
            memoryPanelData.Position = RandomPosition();

        GameObject temp = Instantiate(_uIPrefabLoad.GetPrefab(EUIPrefabType.MemoryPanel), this.gameObject.transform);
        temp.GetComponent<MemoryPanel>().Init(memoryPanelData);
    }
}