using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class DoorEnding : MonoBehaviour
{
    [SerializeField] List<GameObject> _frames;

    int _index;

    private void OnEnable()
    {
        GenericSingleton<AchievementManager>.Instance.UnlockAchievement(EAchievementID.ACH_CUTTER_ENDING);
    }

    public void OnClickNextFrame()
    {
        if (_index >= _frames.Count)
        {
            SceneManager.LoadScene("LobbyScene");
            return;
        }
        _frames[_index].SetActive(true);
        _index++;
    }
}