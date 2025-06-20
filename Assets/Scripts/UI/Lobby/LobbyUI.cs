using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class LobbyUI : MonoBehaviour
{
    Animator _animator;

    async void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("doCredit");
        await GenericSingleton<PrefabManager>.Instance.LoadPrefab();
        _animator.SetBool("isShow", true);
    }

    #region Button
    public void ClickStartButton()
    {
        SceneManager.LoadScene("IngameScene");
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }
    #endregion
}