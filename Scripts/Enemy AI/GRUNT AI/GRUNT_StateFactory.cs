using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRUNT_StateFactory : EnemyStateFactory
{
    public GRUNT_StateFactory(EnemyStateManager stateManager) : base(stateManager)
    {
    }
    public override EnemyBaseState Idle()
    {
        return new EnemyIdleState(stateManager, this);
    }
    public override EnemyBaseState Aggro()
    {
        return new EnemyAggroState(stateManager, this);
    }
    public override EnemyBaseState InRange()
    {
        return new EnemyInRangeState(stateManager, this);
    }
    public override EnemyBaseState Staggered()
    {
        return new EnemyStaggeredState(stateManager, this);
    }
    public override EnemyBaseState Chasing()
    {
        return new EnemyChasingState(stateManager, this);
    }
    public override EnemyBaseState Attacking()
    {
        return new EnemyAttackState(stateManager, this);
    }
    public override EnemyBaseState Recovering()
    {
        return new EnemyRecoveryState(stateManager, this);
    }
    public override EnemyBaseState FindingAttack()
    {
        return new EnemyFindAttackState(stateManager, this);
    }
    public override EnemyBaseState HitState()
    {
        return new EnemyHitState(stateManager, this);
    }

    public override EnemyBaseState Circling()
    {
        throw new System.NotImplementedException();
    }
    public override EnemyBaseState ReactToAttack()
    {
        return new EnemyReactToAttackState(stateManager,this);
    }
}


