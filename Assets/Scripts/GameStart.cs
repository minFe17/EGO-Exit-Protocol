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
    }
}