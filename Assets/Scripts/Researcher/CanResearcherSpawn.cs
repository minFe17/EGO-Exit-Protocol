using Utils;

public class CanResearcherSpawn : IMediatorEvent
{
    ResearcherManager _researcherManager;

    public void Init(ResearcherManager researcherManager)
    {
        _researcherManager = researcherManager;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.EndDialog, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _researcherManager.Spawn();
    }
}