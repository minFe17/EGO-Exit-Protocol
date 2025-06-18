using System.Threading.Tasks;
using UnityEngine;

public class PlayerPrefabLoad : PrefabLoadBase
{
    GameObject _playerPrefab;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "Player";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _playerPrefab = await _addressableManager.GetAddressableAsset<GameObject>(_name);
    }

    public override GameObject GetPrefab()
    {
        return _playerPrefab;
    }
}