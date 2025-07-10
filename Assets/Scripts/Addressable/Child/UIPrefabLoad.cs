using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIPrefabLoad : PrefabLoadBase
{
    Dictionary<EUIPrefabType, GameObject> _uiPrefabDict = new Dictionary<EUIPrefabType, GameObject>();

    string GetAddressableKey(EUIPrefabType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)EUIPrefabType.Max; i++)
        {
            string key = GetAddressableKey((EUIPrefabType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_uiPrefabDict.ContainsKey((EUIPrefabType)i))
                _uiPrefabDict.Add((EUIPrefabType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EUIPrefabType key = (EUIPrefabType)(object)type;
        return _uiPrefabDict[key];
    }
}