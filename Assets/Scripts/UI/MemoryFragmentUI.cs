using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

public class MemoryFragmentUI : MonoBehaviour
{
    [SerializeField] List<Image> _buttonList;
    [SerializeField] List<BoardUI> _boardList;
    [SerializeField] Color _normalColor;
    [SerializeField] Color _selectColor;

    MediatorManager _mediatorManager;
    int _currentIndex;

    void OnEnable()
    {
        SelectButton(0);
        if (_mediatorManager == null)
            _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Notify(EMediatorEventType.TimePause);
    }

    void SelectButton(int index)
    {
        _buttonList[_currentIndex].color = _normalColor;
        _boardList[_currentIndex].gameObject.SetActive(false);
        _currentIndex = index;
        _buttonList[_currentIndex].color = _selectColor;
        _boardList[_currentIndex].gameObject.SetActive(true);
    }

    #region Button OnClick
    public void ClickButton(int index)
    {
        if (_currentIndex == index)
            return;
        SelectButton(index);
    }

    public void ClickUndoButton()
    {
        _boardList[_currentIndex].Restore();
    }
    #endregion

    #region Input System
    void OnClose()
    {
        this.gameObject.SetActive(false);
        _mediatorManager.Notify(EMediatorEventType.TimeResume);
    }

    void OnChangePage(InputValue value)
    {
        int index = _currentIndex + (int)value.Get<float>();
        if (index < 0)
            index = 0;
        if (index >= _buttonList.Count)
            index = _buttonList.Count - 1;

        SelectButton(index);
    }
    #endregion
}