using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraPrefabLoad : PrefabLoadBase
{
    Dictionary<ECameraPrefabType, GameObject> _cameraPrefabDict = new Dictionary<ECameraPrefabType, GameObject>();

    string GetAddressableKey(ECameraPrefabType type)
    {
        return $"{type}";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        for (int i = 0; i < (int)ECameraPrefabType.Max; i++)
        {
            string key = GetAddressableKey((ECameraPrefabType)i);
            GameObject prefab = await _addressableManager.GetAddressableAsset<GameObject>(key);
            if (prefab != null && !_cameraPrefabDict.ContainsKey((ECameraPrefabType)i))
                _cameraPrefabDict.Add((ECameraPrefabType)i, prefab);
        }
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        ECameraPrefabType key = (ECameraPrefabType)(object)type;
        return _cameraPrefabDict[key];
    }
}