using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ResearcherManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // ╫л╠шео
    List<GameObject> _researcherList = new List<GameObject>();
    GameObject _researcherPrefab;
    GameObject _bulletPrefab;

    public void Init()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.SpawnResearcher, this);
        if (_researcherPrefab != null)
            return;
        PrefabLoadBase ResearcherLoadbase = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Researcher);
        _researcherPrefab = ResearcherLoadbase.GetPrefab(EResearcherPrefabType.Researcher);
        //_bulletPrefab = ResearcherLoadbase.GetPrefab(EResearcherPrefabType.Bullet);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        Vector3 position = Vector3.zero;
        if (data != null)
            position = (Vector3)(object)data;
        _researcherList.Add(Instantiate(_researcherPrefab, position, Quaternion.identity));
    }

    void ILoopObject.OnLoopEvent()
    {
        for (int i = 0; i < _researcherList.Count; i++)
        {
            Destroy(_researcherList[i]);
        }
        _researcherList.Clear();
    }
}