using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    int _count=0; //used for looping through hit animations
    float _startTime; //used to decide when to exit state
    public EnemyHitState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 1;
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //if time is over, change to EnemyFindAttackState
        if (Time.time > _startTime + stateManager.HitAnimations[_count].length + 0.5f)
        {
            ChangeState(stateFactory.InRange());
        }
    }

    public override void EnterState()
    {
        stateManager.Animator.speed = 0.75f;
        stateManager.Busy = true;
        _startTime = Time.time;
    }

    public override void ExitState()
    {
        stateManager.Animator.speed = 1f;
        stateManager.Busy = false;
    }  

    public override void InitializeSubState()
    {
        currentSubState = null;
    }

    public override void UpdateState()
    {
    }

    public override void WeaponCollide(Collider collider)
    {
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); //inherrited TakeDamage() method

        //starts hit animation
        stateManager.Animator.CrossFade("GetHit" + (_count + 1).ToString(), 0.1f);
        _count++;
        _startTime = Time.time;
        if(_count >= stateManager.HitAnimations.Length) _count = 0;
        //play correct (go through list)  hit animation
    }
}
