using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class SpawnMapTile : MonoBehaviour
{
    [SerializeField] Tilemap _spawnTile;

    private void Awake()
    {
        GenericSingleton<CameraManager>.Instance.CameraMemento.LoopTilemap = _spawnTile;
    }
}
