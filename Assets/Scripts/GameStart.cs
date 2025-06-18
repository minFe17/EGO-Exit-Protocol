using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    async void Start()
    {
        GenericSingleton<TimeManager>.Instance.Init();
        GenericSingleton<LoopManager>.Instance.Init();
        GenericSingleton<JsonManager>.Instance.Init();
        await GenericSingleton<MemoryManager>.Instance.Init();
        CreatePrefab();
    }

    void CreatePrefab()
    {
        GenericSingleton<PlayerManager>.Instance.Init();
        GenericSingleton<CameraManager>.Instance.Init();
    }
}