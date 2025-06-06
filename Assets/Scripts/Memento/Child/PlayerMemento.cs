using UnityEngine;
using Utils;

public class PlayerMemento : MonoBehaviour, IMementoBase
{
    MementoManager _mementoManager;
    Vector3 _playerStartPos;

    public Vector3 PlayerStartPos { get => _playerStartPos; }

    void Start()
    {
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        Init();
        Save();
    }

    public void Init()
    {
        _mementoManager.PlayerMemento = this;
    }

    public void Save()
    {
        _playerStartPos = Vector3.zero;
    }
}