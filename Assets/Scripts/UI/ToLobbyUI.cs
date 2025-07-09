using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ToLobbyUI : MonoBehaviour
{
    #region Button Event
    public void OnClickYesButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClickNobutton()
    {
        gameObject.SetActive(false);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
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