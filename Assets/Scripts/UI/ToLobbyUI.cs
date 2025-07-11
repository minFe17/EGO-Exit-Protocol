using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ToLobbyUI : MonoBehaviour
{
    #region Button Event
    public void OnClickYesButton()
    {
        GenericSingleton<DialogManager>.Instance.Clear();
        GenericSingleton<MediatorManager>.Instance.ClearMediatorEvent();
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClickNobutton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion

    #region Input System
    void OnClose()
    {
        if(gameObject.activeSelf)
            OnClickNobutton();
    }
    #endregion
}