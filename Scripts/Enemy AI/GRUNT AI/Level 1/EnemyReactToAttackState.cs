using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactToAttackState : EnemyBaseState
{
    public EnemyReactToAttackState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //changes to inrange when player is finished attacking
        if (!stateManager.player.GetComponent<PlayerStateManager>().IsAttacking)
        {
            ChangeState(stateFactory.InRange());
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
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
