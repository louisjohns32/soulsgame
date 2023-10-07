using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANOID_SB_AttemptBlock : EnemyBaseState
{
    public HUMANOID_SB_AttemptBlock(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 2;
    }

    public override void CheckSwitchStates()
    {
        //change states when player stops attacking
        if (!stateManager.player.GetComponent<PlayerStateManager>().IsAttacking)
        {
            stateManager.ShieldCollider.DisableCollision(); //shield collider no longer active
            CurrentSuperState.ChangeState(stateFactory.InRange());
        }
    }

    public override void EnterState()
    {
        stateManager.ShieldCollider.EnableCollision(); //sets shield collider to active
    }

    public override void ExitState()
    {
        stateManager.ShieldCollider.DisableCollision();
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
