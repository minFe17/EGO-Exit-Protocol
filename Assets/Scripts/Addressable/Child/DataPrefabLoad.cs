using System.Threading.Tasks;
using UnityEngine;

public class DataPrefabLoad : PrefabLoadBase
{
    TextAsset _memoryData;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "MemoryData";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _memoryData = await _addressableManager.GetAddressableAsset<TextAsset>(_name);
    }

    public override T GetPrefab<T>()
    {
        if (typeof(T) == typeof(TextAsset))
            return (T)(object)_memoryData;
        return default(T);
    }
}