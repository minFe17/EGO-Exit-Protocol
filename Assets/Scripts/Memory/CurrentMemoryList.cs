using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentMemoryList
{
    // ������ �̱���
    [SerializeField] List<MemoryPanelData> _currentMemoryData = new List<MemoryPanelData>();

    public List<MemoryPanelData> CurrtenMemoryData { get => _currentMemoryData; }
}