using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class StairSetting
{
    [SerializeField] Tilemap _targetTilemap;
    [SerializeField] Transform _targetPos;
    [SerializeField] float _cameraPosY;

    public Tilemap TargetTilemap { get => _targetTilemap; }
    public Transform TargetPos { get => _targetPos; }
    public float CameraPosY { get => _cameraPosY; }
}