using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AssistantPrefabLoad : PrefabLoadBase
{
    Dictionary<EAssistantPrefabType, string> _assistantPrefabNameDict;
    Dictionary<EAssistantPrefabType, GameObject> _assistantPrefabDict;


    public override void Init()
    {
        base.Init();
        _assistantPrefabNameDict = new Dictionary<EAssistantPrefabType, string>
        {
            {EAssistantPrefabType.Assistant, "Assistant" },
            {EAssistantPrefabType.Rope, "Rope" }
        };
    }
    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _assistantPrefabDict = new Dictionary<EAssistantPrefabType, GameObject>
        {
            {EAssistantPrefabType.Assistant, await _addressableManager.GetAddressableAsset<GameObject>(_assistantPrefabNameDict[EAssistantPrefabType.Assistant])},
            {EAssistantPrefabType.Rope, await _addressableManager.GetAddressableAsset<GameObject>(_assistantPrefabNameDict[EAssistantPrefabType.Rope]) }
        };
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EAssistantPrefabType key = (EAssistantPrefabType)(object)type;
        return _assistantPrefabDict[key];
    }
}