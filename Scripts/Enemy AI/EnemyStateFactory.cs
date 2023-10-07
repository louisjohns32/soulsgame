using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base state factory which all factories inherrit from. 
public abstract class EnemyStateFactory 
{
    protected EnemyStateManager stateManager;

    public EnemyStateFactory(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public abstract EnemyBaseState Idle();
    public abstract EnemyBaseState Aggro();
    public abstract EnemyBaseState InRange();
    public abstract EnemyBaseState Staggered();
    public abstract EnemyBaseState Chasing();
    public abstract EnemyBaseState Attacking();
    public abstract EnemyBaseState Recovering();
    public abstract EnemyBaseState FindingAttack();
    public abstract EnemyBaseState HitState();
    public abstract EnemyBaseState Circling();
    public abstract EnemyBaseState ReactToAttack();
    
    //states which default to null - most states are unable to have these substates
    public virtual EnemyBaseState AttemptingBlock()
    {
        Debug.LogError("This enemy cannot block");
        return null;
    }
}
