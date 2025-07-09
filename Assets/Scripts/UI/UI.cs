using UnityEngine;
using Utils;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject _memoryFragment;
    [SerializeField] BoardUI _memoryBoard;
    [SerializeField] BoardUI _evidenceBoard;
    [SerializeField] GameObject _dialPanel;
    [SerializeField] GameObject _phonePanel;
    [SerializeField] GameObject _toLoobyPanel;

    DialEvent _dialEvent = new DialEvent();
    PhoneEvent _phoneEvent = new PhoneEvent();

    void Awake()
    {
        _memoryBoard.Init();
        _evidenceBoard.Init();
        _dialEvent.Init(_dialPanel);
        _phoneEvent.Init(_phonePanel);
    }

    #region Input System
    void OnClose()
    {
        if (_memoryFragment.activeSelf)
            return;
        if (_dialPanel.activeSelf || _phonePanel.activeSelf)
            return;

        _toLoobyPanel.SetActive(true);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimePause);
    }
    #endregion
}