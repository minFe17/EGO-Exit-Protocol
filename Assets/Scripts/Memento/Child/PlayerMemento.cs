using UnityEngine;
using Utils;

public class PlayerMemento : MonoBehaviour, IMementoBase
{
    MementoManager _mementoManager;
    Vector3 _playerStartPos = new Vector3(-3f, -2.13f);

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
    }
}