using System.Threading.Tasks;
using UnityEngine;

public class CameraPrefabLoad : PrefabLoadBase
{
    GameObject _cameraPrefab;
    string _name;

    public GameObject CameraPrefab { get => _cameraPrefab; }

    public override void Init()
    {
        base.Init();
        _name = "MainCamera";
    }

    public override async Task LoadPrefab()
    {
        _cameraPrefab = await _addressableManager.GetAddressableAsset<GameObject>(_name);
    }
}