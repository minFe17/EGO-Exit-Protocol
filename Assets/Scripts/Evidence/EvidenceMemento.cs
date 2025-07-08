using UnityEngine;

public class EvidenceMemento : MonoBehaviour
{
    public Vector2 Position { get; }

    public EvidenceMemento(Vector2 position)
    {
        Position = position;
    }
}