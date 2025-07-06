using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneEnding : MonoBehaviour
{
    public void OnClickEndinPanel()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}