using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class MemoryRepository : MonoBehaviour
{
    // 읽기 전용
    [SerializeField] List<MemoryData> _readDataList = new List<MemoryData>();

    Dictionary<EMemoryType, MemoryData> _allMemoryData = new Dictionary<EMemoryType, MemoryData>();
    CurrentMemoryList _currentMemoryList;
    public List<MemoryData> ReadDataList { get => _readDataList; }

    public void Init()
    {
        _currentMemoryList = DataSingleton<CurrentMemoryList>.Instance;
        GenericSingleton<JsonManager>.Instance.ReadData.ReadMemoryData(this);
        for (int i = 0; i < _readDataList.Count; i++)
        {
            _readDataList[i].Init();
            _allMemoryData.Add(_readDataList[i].Type, _readDataList[i]);
        }
    }

    public void CreateCurrentMemory()
    {
        foreach (MemoryPanelData data in _currentMemoryList.CurrtenMemoryData)
        {
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.CreateMemoryPanel, data);
        }
    }

    public MemoryData GetMemoryData(EMemoryType key)
    {
        return _allMemoryData[key];
    }

    public bool ContainsMemoryType(EMemoryType type)
    {
        foreach(MemoryPanelData data in _currentMemoryList.CurrtenMemoryData)
        {
            if (data.MemoryType == type)
                return true;
        }
        return false;
    }
}