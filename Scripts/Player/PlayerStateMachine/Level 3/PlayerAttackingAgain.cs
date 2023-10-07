using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Serves as a bridge from previous attack to next attack in combo
public class PlayerAttackingAgain : PlayerBaseState
{
    public PlayerAttackingAgain(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (Time.time >= stateManager.AttackStartTime + stateManager.AttackSpeed) //attacks again, if previous attack has fully finished
        {
            ChangeState(stateFactory.AttackingSub1());
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        //updates animator
        stateManager.PlayerAnimator.SetBool("comboing", true);
        stateManager.PlayerAnimator.SetBool(stateManager.LastAttack.ToString(), false);

        //loops through player's attacks
        if (((int)stateManager.LastAttack) >= Enum.GetValues(typeof(LightAttacks)).Length - 1)
        {
            stateManager.LastAttack = LightAttacks.attack1;
        }
        else
        {
            stateManager.LastAttack = (LightAttacks)Enum.ToObject(typeof(LightAttacks), ((int)stateManager.LastAttack) + 1);
        }
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }

}
