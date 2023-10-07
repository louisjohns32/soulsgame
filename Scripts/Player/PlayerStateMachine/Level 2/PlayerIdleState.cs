using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //checks input and changes state accordingly
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            ChangeState(stateFactory.Walking());
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChangeState(stateFactory.Attacking());
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
        stateManager.PlayerAnimator.SetFloat("velX", 0, 0.1f, Time.deltaTime);
        stateManager.PlayerAnimator.SetFloat("velZ", 0, 0.1f, Time.deltaTime);
        stateManager.PlayerAnimator.SetFloat("vel", 0, 0.1f, Time.deltaTime);
    }
}
