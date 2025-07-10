using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogDataPrefabLoad : PrefabLoadBase
{
    Dictionary<EDialogType, string> _dialogDataPrefabNameDict;
    Dictionary<EDialogType, TextAsset> _dialogDataPrefabDict;

    public override void Init()
    {
        base.Init();
        _dialogDataPrefabNameDict = new Dictionary<EDialogType, string>
        {
            {EDialogType.Loop, "LoopDialog" },
            {EDialogType.Door, "DoorDialog" },
            {EDialogType.DoorHasKey, "DoorHasKeyDialog" },
            {EDialogType.DoorHasBoltCutter, "DoorHasBoltCutterDialog" },
            {EDialogType.DoorNoItem, "DoorNoItemDialog" },
            {EDialogType.DoorMainGate, "DoorMainGateDialog" },
            {EDialogType.Rooftop, "RooftopDialog" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _dialogDataPrefabDict = new Dictionary<EDialogType, TextAsset>
        {
            {EDialogType.Loop, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.Loop])},
            {EDialogType.Door, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.Door])},
            {EDialogType.DoorHasKey, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.DoorHasKey])},
            {EDialogType.DoorHasBoltCutter, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.DoorHasBoltCutter])},
            {EDialogType.DoorNoItem, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.DoorNoItem])},
            {EDialogType.DoorMainGate, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.DoorMainGate])},
            {EDialogType.Rooftop, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.Rooftop])}
        };
    }

    public override TextAsset GetPrefabTextAsset<TEnum>(TEnum type)
    {
        EDialogType key = (EDialogType)(object)type;
        return _dialogDataPrefabDict[key];
    }
}
