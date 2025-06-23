using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ZoneManager : MonoBehaviour
{
    // ╫л╠шео
    Dictionary<EZoneType, Zone> _zoneDict = new();
    Dictionary<EZoneType, List<ZoneLink>> _zoneLinkDict = new();
    Zone _playerZone;

    public Zone PlayerZone { get => _playerZone; }

    Dictionary<EZoneType, EZoneType> BFS(EZoneType start)
    {
        Queue<EZoneType> queue = new Queue<EZoneType>();
        Dictionary<EZoneType, EZoneType> cameFrom = new Dictionary<EZoneType, EZoneType>();
        HashSet<EZoneType> visited = new HashSet<EZoneType>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            EZoneType currentZone = queue.Dequeue();
            if (currentZone == _playerZone.ZoneID)
                break;
            if (_zoneLinkDict.TryGetValue(currentZone, out List<ZoneLink> link))
            {
                foreach (ZoneLink zoneLink in link)
                {
                    EZoneType toZone = zoneLink.ToZone;
                    if (visited.Contains(toZone))
                        continue;
                    queue.Enqueue(toZone);
                    visited.Add(toZone);
                    cameFrom[toZone] = currentZone;
                }
            }
        }
        return cameFrom;
    }

    List<EZoneType> ReconstructPath(Dictionary<EZoneType, EZoneType> cameFrom, EZoneType start)
    {
        if (!cameFrom.ContainsKey(_playerZone.ZoneID))
            return null;
        List<EZoneType> path = new List<EZoneType>() { _playerZone.ZoneID };
        EZoneType current = _playerZone.ZoneID;
        while (current != start)
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    public void SetZoneDict(Zone zone) => _zoneDict[zone.ZoneID] = zone;

    public void SetZoneLinkDict(ZoneLink link)
    {
        if (!_zoneLinkDict.ContainsKey(link.FromZone))
            _zoneLinkDict[link.FromZone] = new List<ZoneLink>();
        _zoneLinkDict[link.FromZone].Add(link);
    }

    public void SetPlayerZone(Zone playerZone)
    {
        if (_playerZone == playerZone)
            return;
        _playerZone = playerZone;
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.PlayeMoveOtherZone);
    }

    public ZoneLink GetZoneLink(EZoneType from, EZoneType to)
    {
        if(_zoneLinkDict.TryGetValue(from, out List<ZoneLink> link))
        {
            foreach(ZoneLink zoneLink in link)
            {
                Debug.Log($"{from}, {zoneLink.FromZone}, {to}, {zoneLink.ToZone}");
                if (zoneLink.ToZone == to)
                {
                    Debug.Log(zoneLink);
                    return zoneLink;
                }
            }
        }
        return null;
    }

    public List<EZoneType> FindPath(EZoneType start)
    {
        Dictionary<EZoneType, EZoneType> cameFrom = BFS(start);
        return ReconstructPath(cameFrom, start);
    }
}