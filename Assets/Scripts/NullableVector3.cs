using UnityEngine;

/// <summary>
/// Unity의 JsonUtility는 Vector3? 같은 Nullable 타입 직렬화 X
/// 이 클래스를 사용해 Vector3? 대신 사용하여 직렬화 가능
/// </summary>
[System.Serializable]
public class NullableVector3
{
    [SerializeField] bool _hasValue;    // 값 존재 여부(null 대체)
    [SerializeField] Vector3 _value;    // 값이 존재할 경우 저장되는 Vector3 값

    /// <summary>
    /// Vector3? 값을 입력받아 내부 상태를 초기화
    /// </summary>
    public NullableVector3(Vector3? Input)
    {
        _hasValue = Input.HasValue;

        // input이 null이면 Vector3.zero를, 아니면 input 값을 저장
        _value = Input ?? Vector3.zero;
    }

    /// <summary>
    /// 내부 값을 다시 Vector3? 형식으로 변환
    /// </summary>
    /// <returns> _hasValue가 true면 _value, 아니면 null</returns>
    public Vector3? ToNullable() => _hasValue ? _value : (Vector3?)null;

    public bool HasValue => _hasValue;
    public Vector3? Value => _value;
}