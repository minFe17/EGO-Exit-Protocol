using UnityEngine;
using Utils;

public class LobbyUI : MonoBehaviour
{
    Animator _animator;
    async void Start()
    {
        await GenericSingleton<PrefabManager>.Instance.LoadPrefab();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShow", true);
    }
}