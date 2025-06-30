using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] BoardUI _memoryBoard;
    [SerializeField] BoardUI _evidenceBoard;

    void Awake()
    {
        _memoryBoard.Init(EMediatorEventType.CreateMemoryPanel);
        //_evidenceBoard.Init();
    }
}