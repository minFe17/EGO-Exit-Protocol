using UnityEngine;
using Utils;

public class ResearcherManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
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
        Instantiate(_researcherPrefab, position, Quaternion.identity);
    }
}
