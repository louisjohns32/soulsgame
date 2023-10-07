using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float range = 10f;

    public GameObject focusedEnemy;
    public PlayerManager manager;
    public Transform handTransform;
    public CinemachineFreeLook cineFreelook;
    public GameObject targetGroup;
    public Animator playerAnimator;
    public PlayerMovement playerMovement;

    public Weapon equippedWeapon;

    GameObject weaponObj;
    WeaponCollider weaponCollider;
    

    private List<GameObject> enemiesInRange;

    bool canCombo;
    bool attackAgain;
    LightAttacks lastAttack;

    public float maxHealth; //Move out of player combat TODO
    float health;
    

    private float focusAngle;
    int clicks;
    int qUp;

    bool finishing;

    void Start()
    {
        weaponObj = Instantiate(equippedWeapon.prefab, handTransform);
        enemiesInRange = new List<GameObject>();
        focusedEnemy = null;
        equippedWeapon.weaponType = WeaponType.Sword1H;
        canCombo = false;

        weaponCollider = weaponObj.GetComponent<WeaponCollider>();
        weaponCollider.DisableCollision();

        if(equippedWeapon.animatorOverride != null)
        {
            playerAnimator.runtimeAnimatorController = equippedWeapon.animatorOverride;
        }
        
    }

    void Update()
    {
        UpdateList();
        CheckInput();
        CheckAttacks();
    }

    void UpdateList()
    {
        foreach( GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if((obj.transform.position - transform.position).sqrMagnitude <= range* range) enemiesInRange.Add(obj);
            else if(enemiesInRange.Contains(obj)) enemiesInRange.Remove(obj);
        }
    }
    

    void CheckInput()
    {
        //Left click
        if (!canCombo)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(focusedEnemy != null)
                {
                    /*if(focusedEnemy.GetComponent<EnemyStateManager>().CurrentState == focusedEnemy.GetComponent<EnemyStateManager>().StaggeredState)
                    {
                        StartCoroutine(Finisher());
                    }
                    else
                    {
                        playerAnimator.SetBool("attack1", true);
                        manager.setBusy(true);
                        canCombo = true;
                        lastAttack = LightAttacks.attack1;
                        StartCoroutine(LightAttack());
                    }*/
                    
                }
                else
                {
                    playerAnimator.SetBool("attack1", true);
                    manager.setBusy(true);
                    canCombo = true;
                    lastAttack = LightAttacks.attack1;
                    StartCoroutine(LightAttack());
                }
                

            }
        }
        
        //.left
        //Right click

        //Ability 1

        //Ability 2

        //Ability 3

        //Ability 4

        if (Input.GetKeyDown(KeyCode.Q) && focusedEnemy == null)
        {
            if(enemiesInRange.Count > 0)
            {
                focusedEnemy = enemiesInRange[0];
                manager.Focus(true);
            }
        }
        if(Input.GetKeyUp(KeyCode.Q) && focusedEnemy != null)
        
            focusedEnemy = null;
            manager.Focus(false);
    }
    void CheckAttacks()
    {
        
    }

    void HandleCombo()
    {
        if (canCombo )
        {

        }
    }

    public GameObject GetFocused()
    {
        return focusedEnemy;
    }

    IEnumerator Finisher()
    {
        //lastAttack = LightAttacks.other;
        Debug.Log("FINISHER");
        finishing = true;
        playerAnimator.SetTrigger("finisher");
        weaponCollider.EnableCollision();
        yield return new WaitForSeconds(1f);
        weaponCollider.DisableCollision();
        finishing = false;
    }

    IEnumerator LightAttack()
    {
        float startTime = Time.time;
        float xInput, yInput;
        Vector3 directionVector;
        float attackSpeed;
        if (lastAttack == LightAttacks.attack1)
        {
            attackSpeed = equippedWeapon.lightAttackSpeed1;
        }
        else if (lastAttack == LightAttacks.attack2)
        {
            attackSpeed = equippedWeapon.lightAttackSpeed2;
        }
        else
        {
            attackSpeed = 0;
        }

        while (Time.time < startTime + attackSpeed * 0.3f) // player can change direction of attack
        {
            if(focusedEnemy == null)
            {
                xInput = Input.GetAxisRaw("Horizontal");
                yInput = Input.GetAxisRaw("Vertical");

                
                directionVector = new Vector3(xInput, 0f, yInput).normalized;
                if(directionVector.sqrMagnitude > 0)
                {
                    float rotationAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg + playerMovement.cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref playerMovement.rotationSmoothingVel, playerMovement.rotationSmoothing);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
            }
            else
            {
                transform.LookAt(focusedEnemy.transform.position);
            }
            yield return null;
        }
        weaponCollider.EnableCollision();
        StartCoroutine(playerMovement.Step(transform.forward, attackSpeed * 0.3f, 3f));
        while (Time.time < startTime + attackSpeed)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                attackAgain = true;
            }

            if(!attackAgain && Time.time > startTime + attackSpeed * 0.8f && (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") != 0))
            {
                Debug.Log("Combo ended");
                break;
            }

            yield return null;
        }
        weaponCollider.DisableCollision();
        
        Debug.Log("attack again?" + attackAgain);
        if (attackAgain)  //sets next attack
        {
            playerAnimator.SetBool("comboing", true);
            playerAnimator.SetBool(lastAttack.ToString(), false);
            if (((int)lastAttack) >= Enum.GetValues(typeof(LightAttacks)).Length - 1)
            {
                lastAttack = LightAttacks.attack1;
            }
            else
            {
                lastAttack = (LightAttacks)Enum.ToObject(typeof(LightAttacks), ((int)lastAttack) + 1);
            }
            playerAnimator.SetBool(lastAttack.ToString(), true);
            attackAgain = false;
            StartCoroutine(LightAttack());
        }
        else
        {
            playerAnimator.SetBool("comboing", false);
            Debug.Log(lastAttack.ToString());
            playerAnimator.SetBool(lastAttack.ToString(), false);
            manager.setBusy(false);
            canCombo = false;
            
        }
        
    }

    public void DealDamage(Enemy enemy)
    {
        if(enemy != null)
        {
            if(lastAttack == LightAttacks.attack1)
            {
                equippedWeapon.LightAttack1(enemy);
            }
            else if(lastAttack == LightAttacks.attack2)
            {
                equippedWeapon.LightAttack2(enemy);
            }else if(finishing)
            {
                equippedWeapon.Finish(focusedEnemy.GetComponent<Enemy>());
            }
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage; //add mitigation TODO
    }

}

public enum LightAttacks 
{ 
     attack1,
     attack2
}
