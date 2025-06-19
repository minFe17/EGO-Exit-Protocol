using UnityEngine;

[System.Serializable]
public class MemoryPanelData : MonoBehaviour
{
    [SerializeField] EMemoryType _memoryType;
    [SerializeField] Vector3? _position;

    public MemoryPanelData(EMemoryType memoryType)
    {
        _memoryType = memoryType;
        // �������� ����?
    }

    public EMemoryType MemoryType { get => _memoryType; }
    public Vector3? Position { get => _position; set => _position = value; }
}