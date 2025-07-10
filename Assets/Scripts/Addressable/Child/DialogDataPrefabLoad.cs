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
            {EDialogType.Loop, "LoopDialog" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _dialogDataPrefabDict = new Dictionary<EDialogType, TextAsset>
        {
            {EDialogType.Loop, await _addressableManager.GetAddressableAsset<TextAsset>(_dialogDataPrefabNameDict[EDialogType.Loop])},
        };
    }

    public override TextAsset GetPrefabTextAsset<TEnum>(TEnum type)
    {
        EDialogType key = (EDialogType)(object)type;
        return _dialogDataPrefabDict[key];
    }
}
