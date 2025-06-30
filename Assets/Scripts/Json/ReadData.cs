using System.IO;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
    }

    void ReadJsonData(string path, object dataClass)
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, dataClass);
    }

    void ReadCurrentMemoryData()
    {
        if (!_jsonManager.CheckDataFile(_jsonManager.MemoryDataPath))
            return;
        CurrentMemoryList data = DataSingleton<CurrentMemoryList>.Instance;
        ReadJsonData(_jsonManager.MemoryDataPath, data);
    }

    void ReadAllMemoryData(MemoryRepository memoryRepository)
    {
        PrefabLoadBase dataPrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Data);

        TextAsset textAsset = dataPrefabLoad.GetPrefab<TextAsset>();
        string data = textAsset.text;
        JsonUtility.FromJsonOverwrite(data, memoryRepository);
    }

    public void ReadMemoryData(MemoryRepository memoryRepository)
    {
        ReadAllMemoryData(memoryRepository);
        ReadCurrentMemoryData();
    }

    public void ReadLoopData()
    {
        if (!_jsonManager.CheckDataFile(_jsonManager.LoopDataPath))
            return;
        LoopData data = DataSingleton<LoopData>.Instance;
        ReadJsonData(_jsonManager.LoopDataPath, data);
    }
}