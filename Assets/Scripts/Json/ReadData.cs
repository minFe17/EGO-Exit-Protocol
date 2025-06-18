using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    AddressableManager _addressableManager;
    JsonManager _jsonManager;
    string _allMemoryData;

    public void Init(JsonManager jsonManager)
    {
        _allMemoryData = "MemoryData.json";
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

    async Task ReadAllMemoryData(MemoryRepository memoryRepository)
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        TextAsset temp = await _addressableManager.GetAddressableAsset<TextAsset>(_allMemoryData);
        string data = temp.text;
        JsonUtility.FromJsonOverwrite(data, memoryRepository);
    }

    public async Task ReadMemoryData(MemoryRepository memoryRepository)
    {
        await ReadAllMemoryData(memoryRepository);
        ReadCurrentMemoryData(memoryRepository);
    }
}