using UnityEngine;

public class RoomDoor : DoorBase
{
    [SerializeField] MapTile _leftMap;
    [SerializeField] MapTile _rightMap;

    public override void OnInteract()
    {
        base.OnInteract();
        // Ÿ�� ����(ī�޶� ����)
    }
}