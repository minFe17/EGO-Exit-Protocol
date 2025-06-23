using UnityEngine;

public class AttackState : MonoBehaviour, IResearcherState
{
    Researcher _Researcher;

    public AttackState(Researcher researcher)
    {
        _Researcher = researcher;
    }

    void IResearcherState.Enter()
    {
        _Researcher.ChangeAnimation("isAttack", true);
    }

    void IResearcherState.Loop()
    {

    }

    void IResearcherState.Exit()
    {
        _Researcher.ChangeAnimation("isAttack", false);
    }
}