using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        if (!stateManager.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("GetHit")) //once hit animation has finished, change state to idle
        {
            ChangeState(stateFactory.Idle());
        }
    }

    public override void EnterState()
    {
        stateManager.PlayerAnimator.CrossFade("GetHit", 0.1f);//plays hit animation
    }   

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        currentSubState = null;
    }

    public override void UpdateState()
    {
    }

    
}
