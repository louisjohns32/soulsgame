using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (stateManager.PlayerDistance <= stateManager.AggroRange)
        { //changes state to aggro if player is within aggro rage
          ChangeState(stateFactory.Aggro());
        }
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }

    public override void InitializeSubState()
    {
    }
}
