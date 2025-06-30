using UnityEngine;

[System.Serializable]
public class NullableVector3
{
    [SerializeField] bool _hasValue;
    [SerializeField] Vector3 _value;

    public NullableVector3(Vector3? Input)
    {
        _hasValue = Input.HasValue;
        _value = Input ?? Vector3.zero;
    }

    public Vector3? ToNullable() => _hasValue ? _value : (Vector3?)null;

    public bool HasValue => _hasValue;
    public Vector3? Value => _value;
}