using UnityEngine;
using Utils;

public class ZoneLink : MonoBehaviour
{
    [SerializeField] EZoneType _fromZone;
    [SerializeField] EZoneType _toZoen;
    [SerializeField] Transform _targetPosition;

    public EZoneType FromZone { get => _fromZone; }
    public EZoneType ToZone { get => _toZoen; }
    public Transform TargetPosition { get => _targetPosition; }

    public void Start()
    {
        GenericSingleton<ZoneManager>.Instance.SetZoneLinkDict(this);
    }
}