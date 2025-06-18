using System.Threading.Tasks;
using UnityEngine;

public class PlayerPrefabLoad : PrefabLoadBase
{
    GameObject _playerPrefab;
    string _name;

    public GameObject PlayerPrefab { get => _playerPrefab; }

    public override void Init()
    {
        base.Init();
        _name = "Player";
    }

    public override async Task LoadPrefab()
    {
        _playerPrefab = await _addressableManager.GetAddressableAsset<GameObject>(_name);
    }
}