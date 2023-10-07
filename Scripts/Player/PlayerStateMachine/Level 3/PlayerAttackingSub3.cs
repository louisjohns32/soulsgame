using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//AttackingSub3 - attack is now finished, player can still combo by attacking again, or cancel the combo by moving or waiting too long
public class PlayerAttackingSub3 : PlayerBaseState
{
    public PlayerAttackingSub3(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (stateManager.AttackAgain) //if player should attack again, change state
        {
            ChangeState(stateFactory.AttackingAgain());
        }
        if (Time.time >= stateManager.AttackStartTime + stateManager.AttackSpeed) //if time has run out, exit attack state 
        {
            currentSuperState.ChangeState(stateFactory.Idle());
        }
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)) //if player moves, exit attack state
        {
            currentSuperState.ChangeState(stateFactory.Idle());
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        stateManager.PlayerAnimator.SetBool(stateManager.LastAttack.ToString(), false);
        stateManager.PlayerAnimator.SetBool("comboing", false);
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0)) //if player left clicks, they should attack again
        {
            stateManager.AttackAgain = true;
        }
    }
}
