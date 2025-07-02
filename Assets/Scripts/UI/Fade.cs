using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Fade : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Image _fadeImage;

    Material _material;
    float _targetFadeValue = 1.5f;
    float _time;
    bool _isFade;
    bool _isFadeOut;

    void Start()
    {
        _material = _fadeImage.material;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.StartFade, this);
        _isFade = true;
    }

    void Update()
    {
        if (!_isFade)
            return;

        if (_isFadeOut)
            FadeOut();
        else
            FadeIn();
        _material.SetFloat("_Fade", _time);
    }

    public void FadeIn()
    {
        _time += Time.deltaTime / 2;
        if (_time >= _targetFadeValue)
        {
            _time = _targetFadeValue;
            _targetFadeValue = 0f;
            _isFade = false;
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.EndFade);
        }
    }

    void FadeOut()
    {
        _time -= (Time.deltaTime * 2);
        if (_time <= 0)
        {
            _time = 0;
            _isFadeOut = false;
            _targetFadeValue = 2f;
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.LoopEvent);
        }
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _isFade = true;
        _isFadeOut = true;
    }
}