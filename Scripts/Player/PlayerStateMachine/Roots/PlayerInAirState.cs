using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    float _yVel; //velocity in y direction
    public PlayerInAirState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        if (Physics.CheckSphere(stateManager.GroundTracker.position, 0.1f, stateManager.GroundMask)) //if player is on ground
        {
            ChangeState(stateFactory.Grounded());
        }
    }

    public override void EnterState()
    {
        _yVel = 0;//yvel starts at 0
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        if (stateManager.focusedEnemy != null)
        {
            SetSubState(stateFactory.Focused());

        }
        else
        {
            SetSubState(stateFactory.Unfocused());
        }
    }

    public override void UpdateState()
    {
        _yVel += stateManager.Gravity * Time.deltaTime * 2; //increases yVel by gravity accelleration
        stateManager.CharController.Move(_yVel * Time.deltaTime * Vector3.up); //moves player
    }
}
