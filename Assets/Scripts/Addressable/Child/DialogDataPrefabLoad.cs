using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogDataPrefabLoad : PrefabLoadBase
{
    Dictionary<EDialogType, TextAsset> _dialogDataDict = new Dictionary<EDialogType, TextAsset>();

    string GetAddressableKey(EDialogType type)
    {
        return $"{type}Dialog";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();

        for(int i=0; i<(int)EDialogType.Max; i++)
        {
            string key = GetAddressableKey((EDialogType)i);
            TextAsset textAsset = await _addressableManager.GetAddressableAsset<TextAsset>(key);
            if (textAsset != null && !_dialogDataDict.ContainsKey((EDialogType)i))
                _dialogDataDict.Add((EDialogType)i, textAsset);
        }
    }

    public override TextAsset GetPrefabTextAsset<TEnum>(TEnum type)
    {
        EDialogType key = (EDialogType)(object)type;
        return _dialogDataDict[key];
    }
}
