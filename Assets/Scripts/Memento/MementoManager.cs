using System.Collections.Generic;
using UnityEngine;

public class MementoManager : MonoBehaviour
{
    // ╫л╠шео
    Dictionary<EItemType, ItemMemento> _itemMemento = new Dictionary<EItemType, ItemMemento>();
    PlayerMemento _playerMemen;
    public PlayerMemento PlayerMemento { get => _playerMemen; set => _playerMemen = value;  }
    public Dictionary<EItemType, ItemMemento> ItemMemento { get => _itemMemento; }

    private void Awake()
    {
        SetItemMemento();
    }

    void SetItemMemento()
    {
        for(int i=0; i<(int)EItemType.Max; i++)
            _itemMemento.Add((EItemType)i, new ItemMemento());
    }
}