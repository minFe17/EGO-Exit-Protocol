using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class MemoryRepository : MonoBehaviour
{
    // 읽기 전용
    [SerializeField] List<MemoryData> _readDataList = new List<MemoryData>();
    [SerializeField] HashSet<MemoryPanelData> _currentMemoryData = new HashSet<MemoryPanelData>();

    Dictionary<EMemoryType, MemoryData> _allMemoryData = new Dictionary<EMemoryType, MemoryData>();

    public List<MemoryData> ReadDataList { get => _readDataList; }
    public HashSet<MemoryPanelData> CurrentMemoryData { get => _currentMemoryData; }

    public void Init()
    {
        GenericSingleton<JsonManager>.Instance.ReadData.ReadMemoryData(this);
        CreateCurrentMemory();
        for (int i = 0; i < _readDataList.Count; i++)
        {
            _readDataList[i].Init();
            _allMemoryData.Add(_readDataList[i].Type, _readDataList[i]);
        }
    }

    void CreateCurrentMemory()
    {
        foreach (MemoryPanelData data in _currentMemoryData)
        {
            // 판넬 생성
        }
    }

    public MemoryData GetMemoryData(EMemoryType key)
    {
        return _allMemoryData[key];
    }

    public bool ContainsMemoryType(EMemoryType type)
    {
        foreach(MemoryPanelData data in _currentMemoryData)
        {
            if (data.MemoryType == type)
                return true;
        }
        return false;
    }
}