using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MemoryPanel : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image _image;
    [SerializeField] Text _description;

    MemoryPanelData _memoryPanelData;
    MemoryManager _memoryManager;
    MemoryData _memoryData;

    public void Init(MemoryPanelData memoryPanelData)
    {
        _memoryPanelData = memoryPanelData;
        rectTransform.anchoredPosition = _memoryPanelData.Position.Value;
        _memoryManager = GenericSingleton<MemoryManager>.Instance;
        _memoryData = _memoryManager.MemoryRepository.GetMemoryData(_memoryPanelData.MemoryType);
        _image.sprite = _memoryData.Sprite;
        _description.text = _memoryData.Description;
    }
}