using UnityEngine.Tilemaps;

public class CameraMemento
{
    Tilemap _loopTilemap;

    public Tilemap LoopTilemap { get => _loopTilemap; set => _loopTilemap = value; }
}