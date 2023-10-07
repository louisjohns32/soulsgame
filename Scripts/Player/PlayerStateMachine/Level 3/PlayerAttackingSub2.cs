using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AttackingSub2 - weapon collider enabled in this stage, direction of attack is now fixed 
public class PlayerAttackingSub2 : PlayerBaseState
{
    public PlayerAttackingSub2(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (Time.time > stateManager.AttackStartTime + stateManager.AttackSpeed * 0.8f)
        {
            if (stateManager.AttackAgain) //player can attack again to combo
            {
                ChangeState(stateFactory.AttackingAgain());
            }
            else
            {
                ChangeState(stateFactory.AttackingSub3());
            }
        }
    }

    public override void EnterState()
    {
        stateManager.WeaponCollider.EnableCollision();
    }

    public override void ExitState()
    {
        stateManager.WeaponCollider.DisableCollision();
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateManager.AttackAgain = true;
        }
        stateManager.CharController.Move(stateManager.transform.forward * Time.deltaTime * 3f); //STEP - this is a very basic, unpolished way of stepping 
                                                                                                //TODO- implement steppping as a curve? with different curves for each attack?
    }
}
