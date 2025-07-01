using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] BoardUI _memoryBoard;
    [SerializeField] BoardUI _evidenceBoard;
    [SerializeField] GameObject _dialPanel;
    [SerializeField] GameObject _phonePanel;

    DialEvent _dialEvent = new DialEvent();
    PhoneEvent _phoneEvent = new PhoneEvent();

    void Awake()
    {
        _memoryBoard.Init(EMediatorEventType.CreateMemoryPanel);
        //_evidenceBoard.Init();
        _dialEvent.Init(_dialPanel);
        _phoneEvent.Init(_phonePanel);
    }
}