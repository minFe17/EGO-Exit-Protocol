using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class SpawnMapTile : MonoBehaviour
{
    [SerializeField] Tilemap _spawnTile;

    void Awake()
    {
        GenericSingleton<MementoManager>.Instance.CameraMemento.LoopTilemap = _spawnTile;
    }
}