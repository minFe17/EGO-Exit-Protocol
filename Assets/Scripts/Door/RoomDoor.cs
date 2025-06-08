using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDoor : DoorBase
{
    [SerializeField] Tilemap _leftMap;
    [SerializeField] Tilemap _rightMap;

    public override void OnInteract()
    {
        base.OnInteract();
        _cameraManager.UpdateTileBound(_leftMap, _rightMap);
    }
}