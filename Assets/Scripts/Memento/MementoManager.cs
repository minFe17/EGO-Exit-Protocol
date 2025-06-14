using System.Collections.Generic;
using UnityEngine;

public class MementoManager : MonoBehaviour
{
    // ╫л╠шео
    Dictionary<EItemType, ItemMemento> _itemMemento = new Dictionary<EItemType, ItemMemento>();
    PlayerMemento _playerMemento;
    CameraMemento _cameraMemento = new CameraMemento();
    TimeMemento _timeMemento = new TimeMemento();

    public Dictionary<EItemType, ItemMemento> ItemMemento { get => _itemMemento; }
    public PlayerMemento PlayerMemento { get => _playerMemento; set => _playerMemento = value;  }
    public CameraMemento CameraMemento { get => _cameraMemento; }
    public TimeMemento TimeMemento { get => _timeMemento; }

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