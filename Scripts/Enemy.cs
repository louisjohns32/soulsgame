using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public float health;
    public int posture;

    Animator animator;
    EnemyStateManager stateManager;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<EnemyStateManager>();
        health = maxHealth;
        posture = 3;
    }

    
    void Update()
    {
        
    }

    public void DealDamage(int damage) //reduces health and kills enemy if it runs out
    {
        stateManager.getCurrentState().TakeDamage(damage);

        if (health <= 0) Die();
        
    }
    public void LowerPosture(int amount)
    {
        posture -= amount;
        if(posture <= 0)
        {
            //stateManager.ChangeState(stateManager.StaggeredState);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

}
