using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    Collider weaponCollider;
    PlayerCombat combat;
    PlayerStateManager playerStateManager;

    List<GameObject> enemiesHit;
    void Awake()
    {
        weaponCollider = GetComponent<Collider>(); //gets reference to collider component
        weaponCollider.enabled = false; //weaponcollider disabled by default
        playerStateManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>(); //gets reference to statemanager
        enemiesHit = new List<GameObject>(); //list of enemies hit in an attack
    }
    
    public void EnableCollision()
    {
        weaponCollider.enabled = true;
    }
    public void DisableCollision()
    {
        weaponCollider.enabled = false;
        enemiesHit.Clear(); //resets enemies hit
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyShieldCollider>() != null && !enemiesHit.Contains(other.gameObject.GetComponentInParent<Enemy>().gameObject))
        { //if shield collider is hit, but enemy is not hit, attack is blocked
            float angle = Vector3.Angle((playerStateManager.gameObject.transform.position - other.transform.position), other.transform.forward);
            if(angle > -30 && angle < 30)
            {
                playerStateManager.CurrentState.CurrentSubState.CurrentSubState.BlockAttack(); //calls BlockAttack on the level 2 state (which should only be attacking state if called here
            }
        }
        else if (!enemiesHit.Contains(other.gameObject) && other.gameObject.GetComponent<Enemy>() != null)
        {//enemy is hit and added to enemies hit to avoid repeats
            enemiesHit.Add(other.gameObject);
            playerStateManager.CurrentState.DealDamage(other);
        }
    }
    
}
