using Utils;

public class TimeEventBase
{
    protected TimeManager _timeManager;
    protected MediatorManager _mediatorManager;

    public virtual void Init()
    {
        _timeManager = GenericSingleton<TimeManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }
}