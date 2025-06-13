using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MemoryFragmentUI : MonoBehaviour
{
    [SerializeField] List<Button> _buttonList;
    [SerializeField] List<GameObject> _boardList;

    int _currentIndex;
    void OnEnable()
    {
        _currentIndex = 0;
        _buttonList[_currentIndex].Select();
        _boardList[_currentIndex].SetActive(true);
    }

    public void SelectButton(int index)
    {
        if (_currentIndex == index)
            return;
        _boardList[_currentIndex].SetActive(false);
        _currentIndex = index;
        _boardList[_currentIndex].SetActive(true);
    }

    #region Input System
    void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    void OnChangePage(InputValue value)
    {

    }
    #endregion
}