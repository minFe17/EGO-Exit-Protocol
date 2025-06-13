using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MemoryFragmentUI : MonoBehaviour
{
    [SerializeField] List<Image> _buttonList;
    [SerializeField] List<GameObject> _boardList;
    [SerializeField] Color _normalColor;
    [SerializeField] Color _selectColor;

    int _currentIndex;

    void OnEnable()
    {
        SelectButton(0);
    }

    void SelectButton(int index)
    {
        _buttonList[_currentIndex].color = _normalColor;
        _boardList[_currentIndex].SetActive(false);
        _currentIndex = index;
        _buttonList[_currentIndex].color = _selectColor;
        _boardList[_currentIndex].SetActive(true);
    }

    #region Button OnClick
    public void ClickButton(int index)
    {
        if (_currentIndex == index)
            return;
        SelectButton(index);
    }
    #endregion

    #region Input System
    void OnClose()
    {
        this.gameObject.SetActive(false);
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