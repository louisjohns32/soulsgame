using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//disables player whilst blocked
public class PlayerBlockedState : PlayerBaseState
{
    float _startTime;

    public PlayerBlockedState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //change to idle once block animation is finished
        //changes state after time has passed
        if(Time.time > _startTime + 1f)
        {
            ChangeState(stateFactory.Idle());
        }
    }

    public override void EnterState()
    {
        _startTime = Time.time;
        //Start blocked attack animation - This may have to be done after changing storage of attacks as scriptable objects instead of using an Enum
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
}
