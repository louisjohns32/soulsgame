using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    float _startTime;
    float _rollTimer;
    Vector3 _rollDirection;

    public PlayerRollingState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if(Time.time > _startTime + stateManager.RollTime)//changes state to idle when roll is finished
        {
            ChangeState(stateFactory.Idle());
        }
    }

    public override void EnterState()
    {
        if(stateManager.Stamina >= stateManager.RollCost) //if player has enough stamina to roll
        {
            stateManager.DrainStamina(stateManager.RollCost);
            float _forwardAngle;
            if (stateManager.focusedEnemy != null) //calculates forawrd angle based on enemy pos (forward -> enemy)
            {
                Vector3 _playerEnemyVector3 = stateManager.focusedEnemy.transform.position - stateManager.transform.position;
                Vector2 _playerEnemyVector2 = new Vector2(_playerEnemyVector3.x, _playerEnemyVector3.z);
                _forwardAngle = Vector2.Angle(Vector2.up, _playerEnemyVector2);
                if (_playerEnemyVector3.x < 0)
                {
                    _forwardAngle *= -1;
                }
            }
            else //forward angle based on camera position
            {
                _forwardAngle = stateManager.MainCam.transform.eulerAngles.y;
            }

            //calculates roll vector based on input and relative to forward angle
            float _xInput = Input.GetAxisRaw("Horizontal");
            float _yInput = Input.GetAxisRaw("Vertical");

            Vector3 _inputDir = new Vector3(_xInput, 0f, _yInput).normalized;
            float _angle = Mathf.Atan2(_inputDir.x, _inputDir.z) * Mathf.Rad2Deg + _forwardAngle;
            _startTime = Time.time;
            _rollTimer = stateManager.RollCD;
            stateManager.PlayerAnimator.SetBool("Roll", true);

            _rollDirection = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;
        }
        else
        {
            ChangeState(stateFactory.Idle()); //player cant roll due to low stamina
        }
        
    }

    public override void ExitState()
    {
        stateManager.PlayerAnimator.SetBool("Roll", false);
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        stateManager.CharController.Move(_rollDirection * Time.deltaTime * stateManager.RollSpeed); //moves player
    }
}
