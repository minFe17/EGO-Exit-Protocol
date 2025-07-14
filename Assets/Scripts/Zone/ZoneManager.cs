using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 맵의 Zone 및 ZoneLink(Zone 간 연결)를 관리
/// BBFS 기반 경로 탐색
/// 플레이어 위치 변경 시 이벤트를 발생
/// </summary>
public class ZoneManager : MonoBehaviour
{
    // 싱글턴
    Dictionary<EZoneType, Zone> _zoneDict = new();
    Dictionary<EZoneType, List<ZoneLink>> _zoneLinkDict = new();
    Zone _playerZone;

    public Zone PlayerZone { get => _playerZone; }

    /// <summary>
    /// BFS 알고리즘을 통해 시작 Zone에서 플레이어가 있는 Zone까지의 경로를 탐색
    /// </summary>
    /// <param name="start">탐색 시작 지점</param>
    /// <returns>각 Zone에 도달하기 전 Zone을 저장한 딕셔너리</returns>
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

            // 현재 Zone에서 연결된 Zone들을 순회
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

    /// <summary>
    /// BFS 결과를 바탕으로 경로를 재구성
    /// </summary>
    /// <param name="cameFrom">BFS 경로 딕셔너리</param>
    /// <param name="start">출발 Zone</param>
    /// <returns>플레이어가 위치한 Zone까지의 경로 리스트</returns>
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

    /// <summary>
    /// 플레이어가 위치한 Zone을 갱신하고 알림
    /// </summary>
    public void SetPlayerZone(Zone playerZone)
    {
        if (_playerZone == playerZone)
            return;
        _playerZone = playerZone;
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.PlayeMoveOtherZone);
    }

    /// <summary>
    /// 두 Zone 간 연결(Link)을 가져옴
    /// </summary>
    public ZoneLink GetZoneLink(EZoneType from, EZoneType to)
    {
        if(_zoneLinkDict.TryGetValue(from, out List<ZoneLink> link))
        {
            foreach(ZoneLink zoneLink in link)
            {
                if (zoneLink.ToZone == to)
                {
                    return zoneLink;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 시작 Zone에서 플레이어 Zone까지의 경로를 반환
    /// </summary>
    public List<EZoneType> FindPath(EZoneType start)
    {
        Dictionary<EZoneType, EZoneType> cameFrom = BFS(start);
        return ReconstructPath(cameFrom, start);
    }
}