using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] BoardUI _memoryBoard;
    [SerializeField] BoardUI _evidenceBoard;

    void Start()
    {
        _memoryBoard.Init(EMediatorEventType.CreateMemoryPanel);
        //_evidenceBoard.Init();
    }
}