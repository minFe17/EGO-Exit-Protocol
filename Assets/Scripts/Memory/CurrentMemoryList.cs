using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentMemoryList
{
    // 데이터 싱글턴
    [SerializeField] List<MemoryPanelData> _currentMemoryData = new List<MemoryPanelData>();

    public List<MemoryPanelData> CurrtenMemoryData { get => _currentMemoryData; }
}