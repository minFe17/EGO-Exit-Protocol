using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ResearcherManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // ╫л╠шео
    Queue<Vector3> _spawnPos = new Queue<Vector3>();
    List<GameObject> _researcherList = new List<GameObject>();
    GameObject _researcherPrefab;
    GameObject _bulletPrefab;

    CanResearcherSpawn _canResearcherSpawn = new CanResearcherSpawn();

    public void Init()
    {
        _canResearcherSpawn.Init(this);
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.SpawnResearcher, this);
        if (_researcherPrefab != null)
            return;
        PrefabLoadBase ResearcherLoadbase = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Researcher);
        _researcherPrefab = ResearcherLoadbase.GetPrefab(EResearcherPrefabType.Researcher);
        _bulletPrefab = ResearcherLoadbase.GetPrefab(EResearcherPrefabType.Bullet);
    }

    public void MakeBullet(Transform bulletPos, Vector3 targetPos)
    {
        GameObject bullet = Instantiate(_bulletPrefab, bulletPos.position, Quaternion.identity);
        Vector3 direction = (targetPos - bulletPos.position).normalized;
        bullet.GetComponent<ResearcherBullet>().Init(direction);
    }

    public void Spawn()
    {
        if (_spawnPos.Count <= 0)
            return;
        Vector3 position = _spawnPos.Dequeue();
        _researcherList.Add(Instantiate(_researcherPrefab, position, Quaternion.identity));
        
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        if (data != null)
            _spawnPos.Enqueue((Vector3)data);
        else
            _spawnPos.Enqueue(Vector3.zero);
        if (!GenericSingleton<DialogManager>.Instance.IsDialog)
            Spawn();
    }

    void ILoopObject.OnLoopEvent()
    {
        for (int i = 0; i < _researcherList.Count; i++)
            Destroy(_researcherList[i]);
        _researcherList.Clear();
    }
}