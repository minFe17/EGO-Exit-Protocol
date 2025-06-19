using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraPrefabLoad : PrefabLoadBase
{
    Dictionary<ECameraPrefabType, string> _cameraPrefabNameDict;
    Dictionary<ECameraPrefabType, GameObject> _cameraPrefabDict;

    public override void Init()
    {
        base.Init();
        _cameraPrefabNameDict = new Dictionary<ECameraPrefabType, string>
        {
            {ECameraPrefabType.MainCamera, "MainCamera" },
            {ECameraPrefabType.MemoryCamera, "MemoryCamera" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _cameraPrefabDict = new Dictionary<ECameraPrefabType, GameObject>
        {
            {ECameraPrefabType.MainCamera, await _addressableManager.GetAddressableAsset<GameObject>(_cameraPrefabNameDict[ECameraPrefabType.MainCamera])},
            {ECameraPrefabType.MemoryCamera, await _addressableManager.GetAddressableAsset<GameObject>(_cameraPrefabNameDict[ECameraPrefabType.MemoryCamera]) }
        };
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        ECameraPrefabType key = (ECameraPrefabType)(object)type;
        return _cameraPrefabDict[key];
    }
}