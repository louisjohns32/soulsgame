using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANOID_SB_ReactToAttackState : EnemyBaseState
{
    public HUMANOID_SB_ReactToAttackState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        SetSubState(stateFactory.AttemptingBlock());
    }

    public override void UpdateState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
