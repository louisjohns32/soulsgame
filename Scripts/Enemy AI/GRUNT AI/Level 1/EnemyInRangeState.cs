using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRangeState : EnemyBaseState
{
    public EnemyInRangeState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
    }

    public override void CheckSwitchStates()
    {
        //changes to chasing if not busy and the player is out of range
        if (stateManager.PlayerDistance > 1.5f  && !stateManager.Busy)
        {
            ChangeState(stateFactory.Chasing());
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
        //enemy tries to find attack whilst in range
        SetSubState(stateFactory.FindingAttack());
    }

    public override void UpdateState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
