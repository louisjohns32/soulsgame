using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    float _yVel;
    public PlayerGroundedState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory) 
    { 
        isRootState = true;
        InitializeSubState();
    }
    

    public override void CheckSwitchStates()
    {
        if(!Physics.CheckSphere(stateManager.GroundTracker.position, 1f, stateManager.GroundMask)) //if player is far off ground
        {
            ChangeState(stateFactory.InAir());
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
        if (!Physics.CheckSphere(stateManager.GroundTracker.position, 0.05f, stateManager.GroundMask))
        {//moves player down if they are not on ground - this is meant for slopes/stairs/small
            //drops whereas the inairstate is for large drops
            _yVel += stateManager.Gravity * Time.deltaTime * 2;
            stateManager.CharController.Move(_yVel * Time.deltaTime * Vector3.up);
        }
        else
        {
            _yVel = 0;
        }

        if(Time.time > stateManager.LastDrained + stateManager.RecovCD && stateManager.Stamina < 100)
        {//recovers stamina if stamina has not been recently drained and is not full
            stateManager.RecoverStamina(Time.deltaTime * stateManager.RecovRate);
        }
    }
}
