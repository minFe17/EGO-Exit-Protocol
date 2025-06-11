using UnityEngine;
using Utils;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        GenericSingleton<TimeManager>.Instance.Init();
        GenericSingleton<LoopManager>.Instance.Init();
    }
}