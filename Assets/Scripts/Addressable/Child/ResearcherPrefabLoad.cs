using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResearcherPrefabLoad : PrefabLoadBase
{
    Dictionary<EResearcherPrefabType, GameObject> _researcherPrefabDict = new Dictionary<EResearcherPrefabType, GameObject>();

    string GetAddressableKey(EResearcherPrefabType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EResearcherPrefabType.Max; i++)
        {
            string key = GetAddressableKey((EResearcherPrefabType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_researcherPrefabDict.ContainsKey((EResearcherPrefabType)i))
                _researcherPrefabDict.Add((EResearcherPrefabType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EResearcherPrefabType key = (EResearcherPrefabType)(object)type;
        return _researcherPrefabDict[key];
    }
}