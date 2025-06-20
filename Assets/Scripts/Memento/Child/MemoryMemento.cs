using UnityEngine;

public class MemoryMemento : MonoBehaviour
{
    public Vector2 Position { get; }

    public MemoryMemento(Vector2 position)
    {
        Position = position;
    }
}