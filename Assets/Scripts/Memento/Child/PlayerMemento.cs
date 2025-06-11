using UnityEngine;
using Utils;

public class PlayerMemento : MonoBehaviour
{
    [SerializeField] Vector3 _playerStartPos;
    MementoManager _mementoManager;

    public Vector3 PlayerStartPos { get => _playerStartPos; }

    void Start()
    {
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.PlayerMemento = this;
    }
}