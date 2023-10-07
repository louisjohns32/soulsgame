using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusedState : PlayerBaseState
{
    bool _qUp;
    public PlayerFocusedState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _qUp = true;
            
        }
        else if(Input.GetKeyDown(KeyCode.Q) && _qUp)
        {
            ChangeState(stateFactory.Unfocused());
        }
    }

    public override void EnterState()
    {
        //set focusedEnemy to an enemy in range
        stateManager.focusedEnemy = stateManager.EnemiesInRange[0];
        
        //sets focus camera active
        stateManager.focusCam.gameObject.SetActive(true);

        //updates animator
        stateManager.PlayerAnimator.SetBool("focused", true);
    }

    public override void ExitState()
    {
        stateManager.focusCam.gameObject.SetActive(false);
        stateManager.PlayerAnimator.SetBool("focused", false);
        stateManager.focusedEnemy = null;
    }

    public override void InitializeSubState()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //TODO implement sprinting
            SetSubState(stateFactory.Walking());
        }
        else
        {
            SetSubState(stateFactory.Idle());
        }
    }

    public override void UpdateState()
    {
        stateManager.transform.LookAt(stateManager.focusedEnemy.transform.position);
        stateManager.transform.rotation = Quaternion.Euler(0f, stateManager.transform.rotation.eulerAngles.y, 0f);
        stateManager.ForwardAngle = stateManager.transform.rotation.eulerAngles.y;
    }
}
