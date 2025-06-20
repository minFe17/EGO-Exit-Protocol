using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] EZoneType _zoneID;

    public EZoneType ZoneID { get => _zoneID; }
}