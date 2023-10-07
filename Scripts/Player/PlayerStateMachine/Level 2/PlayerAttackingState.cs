using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackingState : PlayerBaseState
{
    

    public PlayerAttackingState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
    }

    public override void EnterState() //START OF COMBO
    {
        stateManager.IsAttacking = true;
        stateManager.LastAttack = LightAttacks.attack1;
        stateManager.PlayerAnimator.SetBool("comboing", false);

        currentSubState.EnterState();
    }

    public override void ExitState()
    {
        //update animator values
        stateManager.WeaponCollider.DisableCollision();
        stateManager.PlayerAnimator.SetBool("attack1", false); //shouldnt be necessary once ive cleaned up the player animator
        stateManager.PlayerAnimator.SetBool("attack2", false);
        stateManager.PlayerAnimator.SetBool("comboing", false);

        currentSubState.ExitState();
        SetSubState(null);
        stateManager.IsAttacking = false;
    }

    public override void InitializeSubState() 
    {
        SetSubState(stateFactory.AttackingSub1());
    }

    public override void UpdateState()      
    {
      
    }

    public override void DealDamage(Collider enemy)     //TODO Rework damage calculation using player stats.
    {
        float damage;   
        if(stateManager.LastAttack == LightAttacks.attack1)
        {
            damage = 20f;
        }
        else
        {
            damage = 40f;
        }

        enemy.GetComponent<Enemy>().DealDamage((int)damage);
    }

    public override void BlockAttack()
    {
        ChangeState(stateFactory.BlockedState());
    }


}
