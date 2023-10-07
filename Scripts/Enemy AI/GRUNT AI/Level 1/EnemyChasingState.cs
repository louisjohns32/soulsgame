using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
    }

    public override void CheckSwitchStates()
    {
        //changes to InRange depending on playerdistance
        if(stateManager.PlayerDistance <= 1.5f)
        {
            ChangeState(stateFactory.InRange());
        }
    }

    public override void EnterState()
    {
        //updates animator value
        stateManager.Animator.SetBool("chasing", true);
    }

    public override void ExitState()
    {
        //updates animator value
        stateManager.Animator.SetBool("chasing", false);

        stateManager.agent.ResetPath();//removes navmesh target - enemy no longer moving
    }

    public override void InitializeSubState()
    { //enemy will be looking for attack whilst chasing
        currentSubState = stateFactory.FindingAttack();
    }

    public override void UpdateState()
    {
        //sets navmesh target to player's position
        stateManager.agent.SetDestination(stateManager.player.position);
    }

    public override void WeaponCollide(Collider collider)
    {
        
    }
}
