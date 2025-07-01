using UnityEngine;
using UnityEngine.UI;

public class DialNumber : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Dial _dial;
    [SerializeField] int _targetNumber;

    int _number;
    bool _isEqualTargetNumber;
    public bool IsEqualTargetNumber => _isEqualTargetNumber;

    public void UpButton()
    {
        _number = (_number + 1) % 10;
        UpdateTextAndCheck();
    }

    public void DownButton()
    {
        _number = (_number == 0) ? 9 : _number - 1;
        UpdateTextAndCheck();
    }

    void UpdateTextAndCheck()
    {
        _text.text = _number.ToString();

        _isEqualTargetNumber = (_number == _targetNumber);

        if (_isEqualTargetNumber)
            _dial.CheckTargetNumber();
    }
}