using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [SerializeField] PhoneUI _parent;
    [SerializeField] List<GameObject> _panelList;

    GameObject _activeNote;

    #region Button Event
    public void OnClickNote(int index)
    {
        _panelList[index].SetActive(true);
        _activeNote = _panelList[index];
        _parent.SetIsUseApplication(true);
    }
    #endregion

    #region Input System
    void OnClose()
    {
        if (_activeNote != null)
        {
            _activeNote.SetActive(false);
            _activeNote = null;
            _parent.SetIsUseApplication(false);
        }
    }
    #endregion
}