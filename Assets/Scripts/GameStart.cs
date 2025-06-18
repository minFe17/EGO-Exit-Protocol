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
        await GenericSingleton<MemoryManager>.Instance.Init();
        CreatePrefab();
    }

    void CreatePrefab()
    {
        Instantiate(_prefabManager.MapPrefabLoad.MapPrefab);
        GenericSingleton<PlayerManager>.Instance.Init();
        GenericSingleton<CameraManager>.Instance.Init();
    }
}