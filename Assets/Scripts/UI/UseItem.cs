using UnityEngine;
using Utils;

public class UseItem : MonoBehaviour, IMediatorEvent
{
    ItemPanel _itemPanel;

    public void Init(ItemPanel itemPanel)
    {
        _itemPanel = itemPanel;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.UseItem, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        Sprite icon = (Sprite)data;
        _itemPanel.RemoveItemIcon(icon);
    }
}