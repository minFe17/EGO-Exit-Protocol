using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class SpawnMapTile : MonoBehaviour
{
    [SerializeField] Tilemap _spawnTile;

    private void Awake()
    {
        GenericSingleton<MementoManager>.Instance.CameraMemento.LoopTilemap = _spawnTile;
    }
}
