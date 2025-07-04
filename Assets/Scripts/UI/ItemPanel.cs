using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ItemPanel : MonoBehaviour, IMediatorEvent, ILoopObject
{
    [SerializeField] List<Image> _iconList = new List<Image>();

    UseItem _useItem = new UseItem();

    void Start()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.GetItem, this);
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        _useItem.Init(this);
    }

    public void RemoveItemIcon(Sprite sprite)
    {
        for (int i = 0; i < _iconList.Count; i++)
        {
            if (_iconList[i].sprite == sprite)
            {
                _iconList[i].sprite = null;
                break;
            }
        }
    }

    public void RemoveAll()
    {
        for (int i = 0; i < _iconList.Count; i++)
            _iconList[i].sprite = null;
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        Sprite icon = (Sprite)data;
        for (int i = 0; i < _iconList.Count; i++)
        {
            if (_iconList[i].sprite == null)
            {
                _iconList[i].sprite = icon;
                break;
            }
        }
    }

    void ILoopObject.OnLoopEvent()
    {
        RemoveAll();
    }
}