using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AssistantPrefabLoad : PrefabLoadBase
{
    Dictionary<EAssistantPrefabType, GameObject> _assistantPrefabDict = new Dictionary<EAssistantPrefabType, GameObject>();

    string GetAddressableKey(EAssistantPrefabType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EAssistantPrefabType.Max; i++)
        {
            string key = GetAddressableKey((EAssistantPrefabType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_assistantPrefabDict.ContainsKey((EAssistantPrefabType)i))
                _assistantPrefabDict.Add((EAssistantPrefabType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EAssistantPrefabType key = (EAssistantPrefabType)(object)type;
        return _assistantPrefabDict[key];
    }
}