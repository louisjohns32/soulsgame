using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANOIDChasingState : EnemyBaseState
{
    public HUMANOIDChasingState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        if (stateManager.PlayerDistance <= 1.5f)
        {
            ChangeState(stateFactory.InRange());
        }
        //sets state to circling
        ChangeState(stateFactory.Circling());
    }

    public override void EnterState()
    {
        stateManager.Animator.SetBool("walking", true);
    }

    public override void ExitState()
    {
        stateManager.Animator.SetBool("walking", false);
        stateManager.agent.ResetPath();
    }

    public override void InitializeSubState()
    {
        currentSubState = stateFactory.FindingAttack();
    }

    public override void UpdateState()
    {
        stateManager.agent.SetDestination(stateManager.player.position);

    }

    public override void WeaponCollide(Collider collider)
    {

    }
}
