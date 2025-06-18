using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

[System.Serializable]
public class MemoryRepository : MonoBehaviour
{
    // 읽기 전용
    [SerializeField] private List<MemoryData> _readDataList = new List<MemoryData>();
    [SerializeField] private HashSet<EMemoryType> _currentMemoryData = new HashSet<EMemoryType>();

    private Dictionary<EMemoryType, MemoryData> _allMemoryData = new Dictionary<EMemoryType, MemoryData>();

    public List<MemoryData> ReadDataList { get => _readDataList; }
    public HashSet<EMemoryType> CurrentMemoryData { get => _currentMemoryData; }

    public async Task Init()
    {
        await GenericSingleton<JsonManager>.Instance.ReadData.ReadMemoryData(this);
        for (int i = 0; i < _readDataList.Count; i++)
        {
            _readDataList[i].Init();
            _allMemoryData.Add(_readDataList[i].Type, _readDataList[i]);
            Debug.Log(_readDataList[i].Type);
        }
    }

    public MemoryData GetMemoryData(EMemoryType key)
    {
        return _allMemoryData[key];
    }
}