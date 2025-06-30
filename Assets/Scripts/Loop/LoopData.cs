using UnityEngine;

[System.Serializable]
public class LoopData
{
    // 데이터 싱글턴
    [SerializeField] int _loopCount = 1;

    public int LoopCount { get => _loopCount; }

    public void AddLoopCount()
    {
        _loopCount++;
    }
}