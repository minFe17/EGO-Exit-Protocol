using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] Player _player;
    float _loopTime = 30;
    float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _loopTime)
        {
            _player.Loop();
            _timer = 0f;
        }
    }


}
