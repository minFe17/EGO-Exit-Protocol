using System.IO;
using UnityEngine;
using Utils;

public class WriteData
{
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
    }

    void WriteJsonDataBase(object data, string filePath, string cloudName)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        GenericSingleton<SteamCloudManager>.Instance.UploadFileToSteamCloud(filePath, cloudName);
    }

    public void WriteCurrentMemoryData()
    {
        CurrentMemoryList data = DataSingleton<CurrentMemoryList>.Instance;
        WriteJsonDataBase(data, _jsonManager.MemoryDataPath, _jsonManager.MemoryDataName);
    }

    public void WriteLoopData()
    {
        LoopData data = DataSingleton<LoopData>.Instance;
        WriteJsonDataBase(data, _jsonManager.LoopDataPath, _jsonManager.LoopDataPath);
    }

    public void WriteEvidenceData()
    {
        CurrentEvidenceList data = DataSingleton<CurrentEvidenceList>.Instance;
        WriteJsonDataBase(data, _jsonManager.EvidenceDataPath, _jsonManager.EvidenceDataName);
    }
}