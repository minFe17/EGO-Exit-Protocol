using UnityEngine;

[System.Serializable]
public class MemoryPanelData : MonoBehaviour
{
    [SerializeField] EMemoryType _memoryType;
    [SerializeField] Vector3? _position;

    public MemoryPanelData(EMemoryType memoryType)
    {
        _memoryType = memoryType;
        // 포지션은 랜덤?
    }

    public EMemoryType MemoryType { get => _memoryType; }
    public Vector3? Position { get => _position; set => _position = value; }
}