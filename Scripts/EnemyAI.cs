using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//LEGACY SCRIPT - OBSOLETE
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [SerializeField] float aggroRange = 20;
    [SerializeField] float attackRange = 2;
    [SerializeField] float speed = 1;
    [SerializeField] float turnSmoothing = 1;
    [SerializeField] float lightAttackCD = 3f;


    Animator animator;
    float lightAttackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  //change to singleton later TODO
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        agent.speed = speed;
        float playerDistance = Vector3.Distance(player.position, transform.position);
        lightAttackTimer -= Time.deltaTime;
        if(playerDistance < aggroRange)
        {
            animator.SetBool("aggro", true);

            if(playerDistance > attackRange )
            {
                Debug.Log("Player is out of attack range");
                animator.SetBool("chasing", true);
                agent.SetDestination(player.position);
            }
            else
            {
                Debug.Log("Facing target");
                animator.SetBool("chasing", false);
                agent.ResetPath();

                //face target
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSmoothing);
                //Attack player
                if(lightAttackTimer < 0)
                {
                    LightAttack();
                }
                
            }

        }
        else
        {
            animator.SetBool("aggro", false);
        }

        
    }

    private void LightAttack()
    {
        animator.SetTrigger("lightAttack");
        lightAttackTimer = lightAttackCD;

    }
}
