using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindAttackState : EnemyBaseState
{
    List<EnemyAttack> _eligbleAttacks = new List<EnemyAttack>();
    public EnemyFindAttackState(EnemyStateManager stateManager, EnemyStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        level = 2;
    }

    public override void CheckSwitchStates()
    {
        //if an attack is found, change state to attacking
        if (stateManager.CurrentAttack != null)
        {
            ChangeState(stateFactory.Attacking());
        }
    }

    public override void EnterState()
    {
        //current attack is reset
        stateManager.CurrentAttack = null;
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        //loops through enemy attacks to find one to use
        float angle = Vector3.Angle((stateManager.player.transform.position - stateManager.transform.position), stateManager.transform.forward);
        _eligbleAttacks.Clear();
        foreach (EnemyAttack attack in stateManager.AttackList)
        {
            if (stateManager.PlayerDistance <= attack.maxDistance && stateManager.PlayerDistance >= attack.minDistance &&
                angle <= attack.maxAngle && angle >= attack.minAngle)
            {
                if (attack.enabled)
                {
                    for (int i = 0; i < attack.priority; i++)
                    {
                        _eligbleAttacks.Add(attack);
                    }
                }
            }
        }
        if(_eligbleAttacks.Count > 0)
        {
            stateManager.CurrentAttack = _eligbleAttacks[Random.Range(0, _eligbleAttacks.Count)];
        }
    }

    public override void WeaponCollide(Collider collider)
    {
    }
}
