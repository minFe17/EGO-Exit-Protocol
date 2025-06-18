using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

public class WriteData : MonoBehaviour
{
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
    }

    void WriteJsonDataBase(object data, string filePath)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void WriteCurrentMemoryData()
    {
        HashSet<EMemoryType> data = GenericSingleton<MemoryManager>.Instance.MemoryRepository.CurrentMemoryData;
        WriteJsonDataBase(data, _jsonManager.MemoryDataPath);
    }
}