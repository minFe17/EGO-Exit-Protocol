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

    void ReadCurrentMemoryData(MemoryRepository memoryRepository)
    {
        if (!_jsonManager.CheckDataFile(_jsonManager.MemoryDataPath))
            return;
        ReadJsonData(_jsonManager.MemoryDataPath, memoryRepository);
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
        ReadCurrentMemoryData(memoryRepository);
    }
}