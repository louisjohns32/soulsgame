using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecoveryState : EnemyBaseState
{
    float _startTime;
    float _duration;

    public EnemyRecoveryState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 2;
    }

    public override void CheckSwitchStates()
    {
        //changes state once recovered
        if(Time.time > _startTime + stateManager.CurrentAttack.globalRecoveryTime)
        {
            ChangeState(stateFactory.FindingAttack()); 
        }
    }

    public override void EnterState()
    {
        //sets start time to check when state should be changed
        _startTime = Time.time;
        stateManager.StartCoroutine(stateManager.CurrentAttack.Recover()); 
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
