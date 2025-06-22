using UnityEngine;
using Utils;

public class Zone : MonoBehaviour
{
    [SerializeField] EZoneType _zoneID;

    ZoneManager _zoneManager;

    public EZoneType ZoneID { get => _zoneID; }

    private void Start()
    {
        _zoneManager = GenericSingleton<ZoneManager>.Instance;
        _zoneManager.SetZoneDict(this);
    }

    public void SetCurrentZone()
    {
        _zoneManager.SetPlayerZone(this);
    }
}