using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    Vector3 _playerStartPos = new Vector3(-3, -2.13f, 0);

    public Vector3 PlayerStartPos { get => _playerStartPos; set => value = _playerStartPos; }
}