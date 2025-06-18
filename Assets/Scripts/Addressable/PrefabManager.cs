using System.Threading.Tasks;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // ╫л╠шео
    PlayerPrefabLoad _playerPrefabLoad = new PlayerPrefabLoad();
    CameraPrefabLoad _cameraPrefabLoad = new CameraPrefabLoad();

    public PlayerPrefabLoad PlayerPrefabLoad { get => _playerPrefabLoad; }
    public CameraPrefabLoad CameraPrefabLoad { get => _cameraPrefabLoad; }

    public async Task LoadPrefab()
    {
        await _playerPrefabLoad.LoadPrefab();
        await _cameraPrefabLoad.LoadPrefab();
    }
}