using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANOIDCirclingState : EnemyBaseState
{
    float _horizontalDir;
    float _xMovement, _zMovement;
    public HUMANOIDCirclingState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
    }
    public override void CheckSwitchStates()
    {
        //CIRCLE UNTIL A ATTACK IS FOUND THEN MOVE TO InRangeState - (MAYBE CHANGE STATENAME TO ATTACKING? or smth else
        if(currentSubState.GetType() != typeof(EnemyFindAttackState))
        {
            if(currentSubState != null)
            {
                ChangeState(stateFactory.InRange(), subState: currentSubState);
            }
        }
    }

    public override void EnterState()
    {
        //DETERMINE MOVING LEFT OR RIGHT
        _horizontalDir = Random.Range(-1, 1);

        stateManager.Animator.SetBool("walking", true);

        _xMovement = 0.5f * _horizontalDir;
        _zMovement = 0.5f;
        //MAYBE MAKE THE RATIO OF HORIZONTAL:VERTICAL RANDOM (e.g. the enemy can be moving anywhere inbetween straight towards the player, or horizontal(still moving some distance closer to the player)
    }

    public override void ExitState()
    {
        stateManager.Animator.SetBool("walking", false);
    }

    public override void InitializeSubState()
    {
        SetSubState(stateFactory.FindingAttack()); 
    }

    public override void UpdateState()
    {
        //MOVE THE ENEMY
        stateManager.agent.Move((new Vector3(_xMovement, 0, _zMovement) + stateManager.transform.forward) * Time.deltaTime  );
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
