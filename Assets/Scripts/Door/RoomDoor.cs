using UnityEngine;

public class RoomDoor : DoorBase
{
    [SerializeField] MapTile _leftMap;
    [SerializeField] MapTile _rightMap;

    public override void OnInteract()
    {
        base.OnInteract();
        // 타일 관리(카메라 보더)
    }
}