using System.IO;
using UnityEngine;
using Utils;

public class ReadData
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
        byte[] temp = GenericSingleton<SteamCloudManager>.Instance.ReadFileFromSteamCloud(_jsonManager.MemoryDataName);

        CurrentMemoryList data = DataSingleton<CurrentMemoryList>.Instance;

        if (temp == null || temp.Length == 0)
            return;

        string json = System.Text.Encoding.UTF8.GetString(temp);
        JsonUtility.FromJsonOverwrite(json, data);
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
        byte[] temp = GenericSingleton<SteamCloudManager>.Instance.ReadFileFromSteamCloud(_jsonManager.LoopDataName);
        if (temp == null)
            return;
        string json = System.Text.Encoding.UTF8.GetString(temp);
        LoopData data = DataSingleton<LoopData>.Instance;
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void ReadEvidenceData()
    {
        byte[] temp = GenericSingleton<SteamCloudManager>.Instance.ReadFileFromSteamCloud(_jsonManager.EvidenceDataName);
        if (temp == null)
            return;
        string json = System.Text.Encoding.UTF8.GetString(temp);
        CurrentEvidenceList data = DataSingleton<CurrentEvidenceList>.Instance;
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void ReadDialogData(DialogManager dialogManager)
    {
        PrefabLoadBase dialogPrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Dialog);
        
        for (int i=0; i<(int)EDialogType.Max; i++)
        {
            TextAsset textAsset = dialogPrefabLoad.GetPrefabTextAsset((EDialogType)i);
            string data = textAsset.text;
            JsonUtility.FromJsonOverwrite(data, dialogManager.DataList[i]);
        }
    }
}