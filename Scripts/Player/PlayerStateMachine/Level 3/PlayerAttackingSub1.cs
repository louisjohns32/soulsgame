using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AttackingSub1 - first phase of attack, player can still change direction of attack in this stage
public class PlayerAttackingSub1 : PlayerBaseState
{
    float _smoothingVel;

    public PlayerAttackingSub1(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (Time.time >= stateManager.AttackStartTime + stateManager.AttackSpeed * 0.3f) //0.3 is fraction of time of attack speed to stay in this state - come up with a way of calcing this ??? TODO
        {
            ChangeState(stateFactory.AttackingSub2());
        }
    }

    public override void EnterState()
    {
        //init
        stateManager.AttackStartTime = Time.time;
        stateManager.AttackAgain = false;
        stateManager.PlayerAnimator.SetBool(stateManager.LastAttack.ToString(), true);
        

        if (stateManager.LastAttack == LightAttacks.attack1) //P1 Enter state
        {
            stateManager.AttackSpeed = stateManager.EquippedWeapon.lightAttackSpeed1;
        }
        else if (stateManager.LastAttack == LightAttacks.attack2)
        {
            stateManager.AttackSpeed = stateManager.EquippedWeapon.lightAttackSpeed2;
        }
        else
        {
            stateManager.AttackSpeed = 0;
        }
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        if (stateManager.focusedEnemy == null) //turns player towards their intended direction based on input and camera position
        {
            float _xInput = Input.GetAxisRaw("Horizontal");
            float _yInput = Input.GetAxisRaw("Vertical");

            stateManager.AttackDirectionVector = new Vector3(_xInput, 0f, _yInput).normalized;
            if (stateManager.AttackDirectionVector.sqrMagnitude > 0)
            {
                float rotationAngle = Mathf.Atan2(stateManager.AttackDirectionVector.x, stateManager.AttackDirectionVector.z)
                    * Mathf.Rad2Deg + stateManager.MainCam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(stateManager.transform.eulerAngles.y, rotationAngle, 
                    ref stateManager.rotationSmoothingVel, stateManager.RotationSmoothing);
                stateManager.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        else //turns player towards targeted enemy
        {
            stateManager.transform.LookAt(stateManager.focusedEnemy.transform.position);
        }
    }
}
