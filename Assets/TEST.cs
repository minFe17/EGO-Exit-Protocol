using Steamworks;
using UnityEngine;

public class TEST : MonoBehaviour
{
    void Start()
    {
        if (SteamManager.Initialized)
        {
            Debug.Log("Steam 초기화 성공!");
        }
        else
        {
            Debug.LogWarning("Steam 초기화 실패!");
        }
    }
}
