using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PhoneUI : MonoBehaviour, ILoopObject
{
    [SerializeField] List<GameObject> _panelList = new List<GameObject>();

    GameObject _activePanel;

    void Start()
    {
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    #region Button Event
    public void OnClickApplication(int index)
    {
        _panelList[index].SetActive(true);
        _activePanel = _panelList[index];
    }
    #endregion

    #region Input System
    void OnClose()
    {
        if (_activePanel != null)
        {
            _activePanel.SetActive(false);
            _activePanel = null;
        }
        else
        {
            gameObject.SetActive(false);
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
        }
    }
    #endregion

    void ILoopObject.OnLoopEvent()
    {
        _activePanel.SetActive(false);
        gameObject.SetActive(false);
    }
}