using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class MemoryData
{
    [SerializeField] private string _typeText;
    [SerializeField] private string _description;

    private EMemoryType _memoryType;
    string _spritePath;
    Sprite _sprite;
    Texture2D _texture;
    Vector2 _pivot = new Vector2(0.5f, 0.5f);

    public EMemoryType Type { get => _memoryType; }
    public string Description { get => _description; }
    public string SpritePath { get => _spritePath; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    public void Init()
    {
        Enum.TryParse(_typeText, out _memoryType);
        _spritePath = Path.Combine(Application.persistentDataPath, $"{_typeText}.png");
        if (!File.Exists(_spritePath))
            return;
        byte[] imageData = File.ReadAllBytes(_spritePath);
        _texture.LoadImage(imageData);
        _texture.filterMode = FilterMode.Point;
        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), _pivot);
    }
}