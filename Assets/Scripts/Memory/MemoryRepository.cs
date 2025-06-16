using System.Collections.Generic;
using UnityEngine;

public class MemoryRepository : MonoBehaviour
{
    Dictionary<EMemoryType, MemoryData> _allMemoryData = new Dictionary<EMemoryType, MemoryData>();
    HashSet<EMemoryType> _currentMemoryData = new HashSet<EMemoryType>();

    public HashSet<EMemoryType> CurrentMemoryData { get => _currentMemoryData; }

    public MemoryData GetMemoryData(EMemoryType key)
    {
        return _allMemoryData[key];
    }
}