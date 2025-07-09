using UnityEngine;
using UnityEngine.UI;
using Utils;

public class CallUI : MonoBehaviour
{
    [SerializeField] Text _numberText;
    [SerializeField] GameObject _callPanel;

    string _number;

    private void OnEnable()
    {
        _number = null;
        _numberText.text = "---";
    }

    #region Button Event
    public void OnClickNumber(string number)
    {
        _number += number;
        _numberText.text = _number;
    }

    public void OnClickDelete()
    {
        if (string.IsNullOrEmpty(_number))
            return;

        _number = _number.Remove(_number.Length - 1);
        if (_number.Length > 0)
            _numberText.text = _number;
        else
            _numberText.text = "---";
    }

    public void OnClickCall()
    {
        _callPanel.SetActive(true);
        if (_number.Equals("112"))
        {
            int currentEvidenceDataCount = DataSingleton<CurrentEvidenceList>.Instance.CurrentEvidenceData.Count;
            if (currentEvidenceDataCount >= (int)EEvidenceType.Max)
                GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartEndingFade, EEndingType.Phone);
        }
    }

    public void OnClickEndCall()
    {
        _callPanel.SetActive(false);
        _number = null;
        _numberText.text = "---";
    }
    #endregion
}