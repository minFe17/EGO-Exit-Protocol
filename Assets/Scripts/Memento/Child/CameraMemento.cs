using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMemento : MonoBehaviour
{
    Tilemap _loopTilemap;

    public Tilemap LoopTilemap { get => _loopTilemap; set => _loopTilemap = value; }
}