using System.Threading.Tasks;
using UnityEngine;

public class CameraPrefabLoad : PrefabLoadBase
{
    GameObject _cameraPrefab;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "MainCamera";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _cameraPrefab = await _addressableManager.GetAddressableAsset<GameObject>(_name);
    }

    public override GameObject GetPrefab()
    {
        return _cameraPrefab;
    }
}