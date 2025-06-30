using UnityEngine;

[System.Serializable]
public class LoopData
{
    // ������ �̱���
    [SerializeField] int _loopCount = 1;

    public int LoopCount { get => _loopCount; }

    public void AddLoopCount()
    {
        _loopCount++;
    }
}