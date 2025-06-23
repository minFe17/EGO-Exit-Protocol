using UnityEngine;

public class AttackState : MonoBehaviour, IResearcherState
{
    Researcher _researcher;

    float _moveDistance = 6f;

    public AttackState(Researcher researcher)
    {
        _researcher = researcher;
    }

    void IResearcherState.Enter()
    {
        _researcher.ChangeAnimation("isAttack", true);
    }

    void IResearcherState.Loop()
    {
        if (!_researcher.CheckAttackArea(_moveDistance))
            _researcher.ChangeState(EResearcherStateType.Move);
    }

    void IResearcherState.Exit()
    {
        _researcher.ChangeAnimation("isAttack", false);
    }
}