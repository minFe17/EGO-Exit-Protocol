using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnding : MonoBehaviour
{
    [SerializeField] List<GameObject> _frames;

    int _index;

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