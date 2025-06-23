using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResearcherPrefabLoad : PrefabLoadBase
{
    Dictionary<EResearcherPrefabType, string> _researcherPrefabNameDict;
    Dictionary<EResearcherPrefabType, GameObject> _researcherPrefabDict;

    public override void Init()
    {
        base.Init();
        _researcherPrefabNameDict = new Dictionary<EResearcherPrefabType, string>
        {
            {EResearcherPrefabType.Researcher, "Researcher" },
            {EResearcherPrefabType.Bullet, "Bullet" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _researcherPrefabDict = new Dictionary<EResearcherPrefabType, GameObject>
        {
            {EResearcherPrefabType.Researcher, await _addressableManager.GetAddressableAsset<GameObject>(_researcherPrefabNameDict[EResearcherPrefabType.Researcher])},
            //{EResearcherPrefabType.Bullet, await _addressableManager.GetAddressableAsset<GameObject>(_researcherPrefabNameDict[EResearcherPrefabType.Bullet]) }
        };
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EResearcherPrefabType key = (EResearcherPrefabType)(object)type;
        return _researcherPrefabDict[key];
    }
}