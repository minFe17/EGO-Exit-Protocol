using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class EvidencePanel : MonoBehaviour, IMemento
{
    RectTransform _rectTransform;
    BoardUI _board;
    EvidencePanelData _evidencePanelData;
    Rect _rect;
    Vector2 _lastMousePosition;
    Vector2 _halfSize;
    Vector2 _minBoundSize;
    Vector2 _maxBoundSize;

    Stack<EvidenceMemento> _mementoStack = new Stack<EvidenceMemento>();


    public void Init(EvidencePanelData evidencePanelData, BoardUI parent, float yBound)
    {
        _evidencePanelData = evidencePanelData;
        _rectTransform = GetComponent<RectTransform>();
        _board = parent;
        _rect = _board.GetComponent<RectTransform>().rect;
        ShowEvidence();
        CalculateBound(yBound);
    }

    void ShowEvidence()
    {
        _rectTransform.anchoredPosition = _evidencePanelData.Position.Value;
    }

    void CalculateBound(float yBound)
    {
        _halfSize = _rectTransform.rect.size * _rectTransform.pivot;
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

        Vector2 pos = _rectTransform.anchoredPosition + movePos;
        float clampX = Mathf.Clamp(pos.x, _minBoundSize.x, _maxBoundSize.x);
        float clampY = Mathf.Clamp(pos.y, _minBoundSize.y, _maxBoundSize.y);

        Vector2 newPos = new Vector2(clampX, clampY);

        _rectTransform.anchoredPosition = newPos;
        _evidencePanelData.Position = newPos;
        _lastMousePosition = eventData.position;
    }

    public void EndDrag(BaseEventData data)
    {
        GenericSingleton<JsonManager>.Instance.WriteData.WriteEvidenceData();
    }
    #endregion

    #region Interface
    public void Save()
    {
        _mementoStack.Push(new EvidenceMemento(_rectTransform.anchoredPosition));
        _board.Save(this);
    }

    void IMemento.Restore()
    {
        if (_mementoStack.Count > 0)
        {
            EvidenceMemento memento = _mementoStack.Pop();
            _rectTransform.anchoredPosition = memento.Position;
        }
    }
    #endregion
}