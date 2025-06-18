using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIPrefabLoad : PrefabLoadBase
{
    Dictionary<EUIPrefabType, string> _uiPrefabNameDict;
    Dictionary<EUIPrefabType, GameObject> _uiPrefabDict;

    public override void Init()
    {
        base.Init();
        _uiPrefabNameDict = new Dictionary<EUIPrefabType, string>
        {
            {EUIPrefabType.UI, "UI" },
            {EUIPrefabType.MemoryPanel, "MemoryPanel" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _uiPrefabDict = new Dictionary<EUIPrefabType, GameObject>
        {
            {EUIPrefabType.UI, await _addressableManager.GetAddressableAsset<GameObject>(_uiPrefabNameDict[EUIPrefabType.UI])},
            {EUIPrefabType.MemoryPanel, await _addressableManager.GetAddressableAsset<GameObject>(_uiPrefabNameDict[EUIPrefabType.MemoryPanel]) }
        };
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EUIPrefabType key = (EUIPrefabType)(object)type;
        return _uiPrefabDict[key];
    }
}