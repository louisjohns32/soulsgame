using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackState : EnemyBaseState
{



    public EnemyAttackState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 2;
    }

    public override void CheckSwitchStates()
    {
    }



    public override void EnterState()
    {
        stateManager.Busy = true; //enemy is busy
        stateManager.StartCoroutine(attack());
    }



    public override void ExitState()
    {
        stateManager.Busy = false; //enemy no longer busy
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
    }



    public override void WeaponCollide(Collider collider)
    {
        if (collider.tag == "Player") //checks if collision is with player
        {
            stateManager.player.GetComponent<PlayerStateManager>().TakeDamage(30f);
            stateManager.CanCombo = true;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //interrupt attack
        stateManager.Animator.SetBool("comboing", false);
        stateManager.StopAllCoroutines(); //BEWARE this may cause problems in the future if i use other coroutines on teh enemy state manager
    }

    IEnumerator attack()
    {
        stateManager.CanCombo = false;
        stateManager.Animator.CrossFade(stateManager.CurrentAttack.actionAnimation, 0.2f); //calls attack animation

        //wait to enable weapon collider
        yield return new WaitForSeconds(stateManager.CurrentAttack.weaponEnableTime);
        stateManager.WeaponColliders[stateManager.CurrentAttack.colliderIndex].EnableCollision();

        //wait to disable weapon collider
        yield return new WaitForSeconds(stateManager.CurrentAttack.weaponDisableTime);
        stateManager.WeaponColliders[stateManager.CurrentAttack.colliderIndex].DisableCollision();

        //change state to recovering
        ChangeState(stateFactory.Recovering());
    }
}
