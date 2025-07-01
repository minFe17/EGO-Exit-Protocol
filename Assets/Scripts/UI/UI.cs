using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] BoardUI _memoryBoard;
    [SerializeField] BoardUI _evidenceBoard;
    [SerializeField] GameObject _dialPanel;

    DialEvent _dialEvent = new DialEvent();

    void Awake()
    {
        _memoryBoard.Init(EMediatorEventType.CreateMemoryPanel);
        //_evidenceBoard.Init();
        _dialEvent.Init(_dialPanel);
    }
}