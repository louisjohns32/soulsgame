using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory 
{
    PlayerStateManager stateManager;

    public PlayerStateFactory(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(stateManager, this);
    }

    public PlayerBaseState InAir()
    {
        return new PlayerInAirState(stateManager, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(stateManager, this);
    }

    public PlayerBaseState Focused()
    {
        return new PlayerFocusedState(stateManager, this);
    }
    public PlayerBaseState Unfocused()
    {
        return new PlayerUnfocusedState(stateManager, this);
    }
    public PlayerBaseState Walking()
    {
        return new PlayerWalkingState(stateManager, this);
    }
    public PlayerBaseState Attacking()
    {
        return new PlayerAttackingState(stateManager, this);
    }
    public PlayerBaseState AttackingSub1()
    {
        return new PlayerAttackingSub1(stateManager, this);
    }
    public PlayerBaseState AttackingSub2()
    {
        return new PlayerAttackingSub2(stateManager, this);
    }
    public PlayerBaseState AttackingSub3()
    {
        return new PlayerAttackingSub3(stateManager, this);
    }
    public PlayerBaseState AttackingAgain()
    {
        return new PlayerAttackingAgain(stateManager, this);
    }
    public PlayerBaseState Rolling()
    {
        return new PlayerRollingState(stateManager, this);
    }
    public PlayerBaseState HitState()
    {
        return new PlayerHitState(stateManager, this);
    }

    public PlayerBaseState BlockedState()
    {
        return new PlayerBlockedState(stateManager, this);
    }
}
