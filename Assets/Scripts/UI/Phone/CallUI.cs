using UnityEngine;
using UnityEngine.UI;

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
        // 증거를 일정갯수? or 다? 찾았다면 엔딩으로
        // 위 조건이 만족하지 못하면 루프?
    }

    public void OnClickEndCall()
    {
        _callPanel.SetActive(false);
        _number = null;
        _numberText.text = "---";
    }
    #endregion
}