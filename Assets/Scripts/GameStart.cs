using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    PrefabManager _prefabManager;

    async void Start()
    {
        _prefabManager = GenericSingleton<PrefabManager>.Instance;
        await _prefabManager.LoadPrefab();
        GenericSingleton<TimeManager>.Instance.Init();
        GenericSingleton<LoopManager>.Instance.Init();
        GenericSingleton<JsonManager>.Instance.Init();
        GenericSingleton<ResearcherManager>.Instance.Init();
        GenericSingleton<MemoryManager>.Instance.Init();
        CreatePrefab();
        CreateCurrentMomoryData();
    }

    void CreatePrefab()
    {
        Instantiate(_prefabManager.GetPrefabLoad(EPrefabType.Map).GetPrefab());
        Instantiate(_prefabManager.GetPrefabLoad(EPrefabType.UI).GetPrefab(EUIPrefabType.UI));
        CreateAssistant();
        GenericSingleton<PlayerManager>.Instance.Init();
        GenericSingleton<CameraManager>.Instance.Init();
    }

    void CreateAssistant()
    {
        PrefabLoadBase assistantPrefabLoad = _prefabManager.GetPrefabLoad(EPrefabType.Assistant);
        Instantiate(assistantPrefabLoad.GetPrefab(EAssistantPrefabType.Assistant));
        Instantiate(assistantPrefabLoad.GetPrefab(EAssistantPrefabType.Rope));
    }

    void CreateCurrentMomoryData()
    {
        GenericSingleton<MemoryManager>.Instance.MemoryRepository.CreateCurrentMemory();
    }
}