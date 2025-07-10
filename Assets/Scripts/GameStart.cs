using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    PrefabManager _prefabManager;

    void Start()
    {
        _prefabManager = GenericSingleton<PrefabManager>.Instance;
        GenericSingleton<InteractObjectManager>.Instance.Init();
        GenericSingleton<TimeManager>.Instance.Init();
        GenericSingleton<JsonManager>.Instance.Init();
        GenericSingleton<ResearcherManager>.Instance.Init();
        GenericSingleton<MemoryManager>.Instance.Init();
        GenericSingleton<EndingManager>.Instance.Init();
        CreatePrefab();
        ReadData();
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

    void ReadData()
    {
        GenericSingleton<MemoryManager>.Instance.MemoryRepository.CreateCurrentMemory();
        GenericSingleton<LoopManager>.Instance.Init();
        GenericSingleton<DialogManager>.Instance.Init();
    }
}