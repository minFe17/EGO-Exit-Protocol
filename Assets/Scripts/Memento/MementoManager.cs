using UnityEngine;

public class MementoManager : MonoBehaviour
{
    // �̱���
    PlayerMemento _playerMemen;

    public PlayerMemento PlayerMemento { get => _playerMemen; set => _playerMemen = value;  }
}