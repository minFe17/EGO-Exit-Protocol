using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    Vector3 _playerStartPos;

    public Vector3 PlayerStartPos { get => _playerStartPos; set => value = _playerStartPos; }
}