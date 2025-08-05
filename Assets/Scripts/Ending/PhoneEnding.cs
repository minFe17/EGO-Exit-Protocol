using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class PhoneEnding : MonoBehaviour
{
    private void OnEnable()
    {
        GenericSingleton<AchievementManager>.Instance.UnlockAchievement(EAchievementID.ACH_CUTTER_ENDING);
    }

    public void OnClickEndinPanel()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}