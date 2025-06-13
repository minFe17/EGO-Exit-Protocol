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
        // 시간 정지 (TimeManager에 있는 Stop() 함수 실행)
    }
}