using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // ╫л╠шео
    Dictionary<EPrefabType, PrefabLoadBase> _prefabDict;

    void SetDictionary()
    {
        _prefabDict = new Dictionary<EPrefabType, PrefabLoadBase>
        { {EPrefabType.Player, new PlayerPrefabLoad() },
            {EPrefabType.Camera, new CameraPrefabLoad() },
            {EPrefabType.Map, new MapPrefabLoad() },
        };
    }

    public async Task LoadPrefab()
    {
        SetDictionary();
        foreach (PrefabLoadBase prefabLoad in _prefabDict.Values)
            await prefabLoad.LoadPrefab();
    }

    public PrefabLoadBase GetPrefabLoad(EPrefabType key)
    {
        return _prefabDict[key];
    }
}