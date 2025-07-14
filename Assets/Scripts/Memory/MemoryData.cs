using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 기억 조각 정보를 담는 데이터 클래스
/// JSON에서 불러온 문자열 데이터를 파싱
/// 해당 이름으로 저장된 이미지 파일을 가져와 Sprite로 생성
/// </summary>
[System.Serializable]
public class MemoryData
{
    [SerializeField] string _typeText;
    [SerializeField] string _description;

    EMemoryType _memoryType;
    string _spritePath;
    Sprite _sprite;
    Texture2D _texture;
    Vector2 _pivot = new Vector2(0.5f, 0.5f);

    public EMemoryType Type { get => _memoryType; }
    public string Description { get => _description; }
    public string SpritePath { get => _spritePath; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    /// <summary>
    /// 문자열을 Enum으로 파싱
    /// 해당 이름의 이미지 파일 존재 여부 체크
    /// 존재하면 Texture와 Sprite로 로드
    /// </summary>
    public void Init()
    {
        // 문자열로 저장된 타입명을 Enum으로 변환
        Enum.TryParse(_typeText, out _memoryType);

        // 저장된 이미지 파일 경로 생성
        _spritePath = Path.Combine(Application.persistentDataPath, $"{_typeText}.png");

        // 해당 경로에 이미지 파일이 없으면 return
        if (!File.Exists(_spritePath))
            return;

        // 임시 텍스처 생성(크기는 LoadImage로 자동 조정됨)
        _texture = new Texture2D(2, 2);

        // 파일에서 이미지 데이터를 읽어와 텍스처에 적용
        byte[] imageData = File.ReadAllBytes(_spritePath);
        _texture.LoadImage(imageData);

        // 픽셀 선명도를 위해 필터 모드 설정
        _texture.filterMode = FilterMode.Point;

        // 텍스처로부터 Sprite 생성
        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), _pivot);
    }
}