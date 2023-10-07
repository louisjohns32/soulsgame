using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Actions/Attack")]
public class EnemyAttack : EnemyAction
{
    public int priority;        //determines how often this attack should be picked - higher = more often (when meets criteria)
    public float minDistance, maxDistance; // Distance range this attack can be used
    public float minAngle, maxAngle;       // attack FOV (where player is in relation to enemy forward)

    public float globalRecoveryTime; //how long before enemy can do another attack of any type
    public float attackRecoveryTime; //how long before enemy can do another attack of same type
    public float damageMultiplier; //linerarly affects how much damage the attack does

    public float weaponEnableTime, weaponDisableTime; //specifies when the weapon collider should be enabled/disabled
    public int colliderIndex = 0; //specifies which weaponcollider to use

    public bool enabled = true; //whether or not the attack can currently be used

    public IEnumerator Recover() //manages cooldown of attack
    {
        enabled = false;
        yield return new WaitForSeconds(attackRecoveryTime);
        enabled = true;
    }
}
