using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DialogManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    Dictionary<EDialogCharacterType, DialogUI> _characterDialogDict = new Dictionary<EDialogCharacterType, DialogUI>();

    List<DialogData> _dialogList = new List<DialogData>();

    public void Init()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Dialog, this);
        ReadData();
    }

    public void SetDialogUI(EDialogCharacterType key, DialogUI value)
    {
        if (_characterDialogDict.ContainsKey(key))
            return;
        _characterDialogDict.Add(key, value);
    }

    void ReadData()
    {
        GenericSingleton<JsonManager>.Instance.ReadData.ReadDialogData();
    }

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        _dialogList = (List<DialogData>)data;
        StartCoroutine(DialogRoutine());
    }
    #endregion

    #region Coroutine
    IEnumerator DialogRoutine()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimePause);
        for (int i = 0; i < _dialogList.Count; i++)
        {
            if (!_characterDialogDict[_dialogList[i].CharacterType].gameObject.activeSelf)
                _characterDialogDict[_dialogList[i].CharacterType].gameObject.SetActive(true);
            _characterDialogDict[_dialogList[i].CharacterType].ShowDialog(_dialogList[i].Text);
            yield return new WaitForSeconds(1f);
            _characterDialogDict[_dialogList[i].CharacterType].gameObject.SetActive(false);
        }
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.EndDialog);

    }
    #endregion
}