using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
{
    public PlayerWalkingState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //checks input and acts accordingly
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            ChangeState(stateFactory.Idle());
        }
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState(stateFactory.Attacking());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(stateFactory.Rolling());
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
    }

    public override void UpdateState()
    {
        //calculates move direction based on input and camera direction, and if the player has a focus target or not
        Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        float rotationAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg + stateManager.ForwardAngle;
        float angle = Mathf.SmoothDampAngle(stateManager.transform.eulerAngles.y, rotationAngle, 
            ref stateManager.rotationSmoothingVel, stateManager.RotationSmoothing);
        if (stateManager.focusedEnemy != null)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            stateManager.CharController.Move(moveDirection * stateManager.Speed * Time.deltaTime);
        }
        else
        {
            stateManager.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            stateManager.CharController.Move(stateManager.transform.rotation * Vector3.forward * stateManager.Speed * Time.deltaTime);
        }
        stateManager.PlayerAnimator.SetFloat("velX", directionVector.x, 0.1f, Time.deltaTime);
        stateManager.PlayerAnimator.SetFloat("velZ", directionVector.z, 0.1f, Time.deltaTime);
        stateManager.PlayerAnimator.SetFloat("vel", directionVector.magnitude, 0.1f, Time.deltaTime);
    }
}
