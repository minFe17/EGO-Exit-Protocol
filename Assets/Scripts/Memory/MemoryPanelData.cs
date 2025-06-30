using UnityEngine;

[System.Serializable]
public class MemoryPanelData
{
    [SerializeField] int _memoryType;
    [SerializeField] NullableVector3 _position;

    public MemoryPanelData(EMemoryType memoryType)
    {
        _memoryType = (int)memoryType;
        _position = new NullableVector3(null);
        // 포지션은 랜덤?
    }

    public EMemoryType MemoryType { get => (EMemoryType)_memoryType; }
    public Vector3? Position { get => _position.ToNullable(); set => _position = new NullableVector3(value); }
}