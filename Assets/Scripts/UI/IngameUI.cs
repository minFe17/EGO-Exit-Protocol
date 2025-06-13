using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField] Text _loopText;
    [SerializeField] Text _timeText;
    [SerializeField] GameObject _memoryFragmentPanel;

    public void OnClickMemoryButton()
    {
        _memoryFragmentPanel.SetActive(true);
        // �ð� ���� (TimeManager�� �ִ� Stop() �Լ� ����)
    }
}