using UnityEngine;

public class TimeMemento : MonoBehaviour
{
    float _loopTime = 50f;
    bool _isStop = true;
    bool _isLoop = false;

    public float LoopTime { get => _loopTime; }
    public bool IsStop { get => _isStop; }
    public bool IsLoop { get => _isLoop; }
}