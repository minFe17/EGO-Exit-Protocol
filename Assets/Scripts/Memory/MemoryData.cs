using UnityEngine;

[System.Serializable]
public class MemoryData : MonoBehaviour
{
    [SerializeField] EMemoryType _type;
    [SerializeField] string _spritePath;
    [SerializeField] string _description;
    Sprite _sprite;

    // json ���Ϸ� �����
    public string Description { get => _description; }
    public Sprite Sprite { get => _sprite; }
}