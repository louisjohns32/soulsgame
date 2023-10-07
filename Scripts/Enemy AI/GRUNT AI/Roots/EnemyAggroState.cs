using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public EnemyAggroState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        if (stateManager.player.GetComponent<PlayerStateManager>().IsAttacking && !stateManager.Busy)
        { //changes to ReactToAttack if player is attacking and enemy isn't busy
            SetSubState(stateFactory.ReactToAttack());
        }
    }

    public override void EnterState()
    {
        //update animator value
        stateManager.Animator.SetBool("aggro", true);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
        //sets substate based on how far away player is
        if(stateManager.PlayerDistance <= 1.5f)
        {
            SetSubState(stateFactory.InRange());
        }
        else
        {
            SetSubState(stateFactory.Chasing());
        }
    }

    public override void UpdateState()
    {
        if (!stateManager.Busy) //rotates enemy towards player if not busy
        {
            RotateTowardsPlayer();
        }
    }

    public override void WeaponCollide(Collider collider)
    {
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (stateManager.player.transform.position - stateManager.transform.position).normalized; //direction of player from enemy
        direction.y = 0;

        //rotates enemy towards player using smoothing - avoids snappy behaviour, makes movement more realistic
        Quaternion rotation = Quaternion.LookRotation(direction);
        stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, rotation, stateManager.RotateSpeed * Time.deltaTime);
    }
}
