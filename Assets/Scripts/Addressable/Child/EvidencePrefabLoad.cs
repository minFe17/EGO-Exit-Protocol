using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EvidencePrefabLoad : PrefabLoadBase
{
    Dictionary<EEvidenceType, GameObject> _evidencePrefabDict = new Dictionary<EEvidenceType, GameObject>();

    string GetAddressableKey(EEvidenceType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EEvidenceType.Max; i++)
        {
            string key = GetAddressableKey((EEvidenceType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_evidencePrefabDict.ContainsKey((EEvidenceType)i))
                _evidencePrefabDict.Add((EEvidenceType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EEvidenceType key = (EEvidenceType)(object)type;
        return _evidencePrefabDict[key];
    }
}