using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DialogManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    Dictionary<EDialogCharacterType, DialogUI> _characterDialogDict = new Dictionary<EDialogCharacterType, DialogUI>();

    List<DialogDataList> _dataList = new List<DialogDataList>();

    EDialogType _dialogType;

    bool _isDialog;

    public List<DialogDataList> DataList { get => _dataList; }
    public bool IsDialog { get => _isDialog; }

    public void Init()
    {
        Clear();
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Dialog, this);
        SetDataList();
        ReadData();
    }

    public void SetDialogUI(EDialogCharacterType key, DialogUI value)
    {
        if (_characterDialogDict.ContainsKey(key))
            return;
        _characterDialogDict.Add(key, value);
    }

    void SetDataList()
    {
        if (_dataList.Count != 0)
            return;
        for (int i = 0; i < (int)EDialogType.Max; i++)
            _dataList.Add(new DialogDataList());
    }

    void ReadData()
    {
        GenericSingleton<JsonManager>.Instance.ReadData.ReadDialogData(this);
    }

    public void Clear()
    {
        if (_characterDialogDict.Count > 0)
        {
            _characterDialogDict.Clear();
            _dataList.Clear();
        }
    }

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        _dialogType = (EDialogType)data;
        StartCoroutine(DialogRoutine());
    }
    #endregion

    #region Coroutine
    IEnumerator DialogRoutine()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimePause);
        DialogDataList data = _dataList[(int)_dialogType];
        _isDialog = true;

        for (int i = 0; i < data.Lines.Count; i++)
        {
            if (_characterDialogDict.Count == 0)
                yield break;
            if (!_characterDialogDict[data.Lines[i].CharacterType].gameObject.activeSelf)
                _characterDialogDict[data.Lines[i].CharacterType].gameObject.SetActive(true);
            _characterDialogDict[data.Lines[i].CharacterType].ShowDialog(data.Lines[i].Text);
            yield return new WaitForSeconds(1f);
            if (_characterDialogDict.Count == 0)
                yield break;
            _characterDialogDict[data.Lines[i].CharacterType].gameObject.SetActive(false);
        }

        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.TimeResume);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.EndDialog);
        _isDialog = false;
    }
    #endregion
}