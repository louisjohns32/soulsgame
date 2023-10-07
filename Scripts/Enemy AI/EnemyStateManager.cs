using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.TextCore;

//EnemyStateManager manages the whole state machine. It is the only class on the enemy GameObject as a component, and contains all the variables used accross multiple states.
//As little specific code as possible should be within this class. All specific code should be within states. Code here should be intended to be ran at all times no matter the state.

public class EnemyStateManager : MonoBehaviour
{

    //TESTING 
    public Text rootStateText, level1StateText, level2StateText;



    //states
    EnemyBaseState currentState;
    EnemyStateFactory stateFactory;

    //combat
    [SerializeField] EnemyAttack[] attackList;
    EnemyAttack currentAttack;

    [SerializeField] EnemyWeaponCollider[] weaponColliders;
    EnemyShieldCollider shieldCollider;

    [SerializeField] AnimationClip[] hitAnimations;

    bool canCombo;
    float playerDistance;
    [SerializeField] float aggroRange;

    //movement
    [SerializeField] float rotateSpeed = 1f;
    
    //general
    Animator animator;
    public Transform player;
    public NavMeshAgent agent;
    Enemy enemy;

    [SerializeField] int stateFactoryType; //change to enemytype later TODO

    bool busy;

    //getters setters
    public EnemyBaseState getCurrentState(int level = 0)
    { //returns state of specified level (root state if not specified)
        if (level == 0) return currentState;
        else if (level == 1) return currentState.CurrentSubState;
        else if (level == 2) return currentState.CurrentSubState.CurrentSubState;
        else return null;
    }
    public EnemyBaseState CurrentState { set { currentState = value; } }

    public EnemyAttack[] AttackList { get { return attackList; }  }
    public Animator Animator { get { return animator; } }
    public Enemy Enemy { get { return enemy; } }
    public EnemyAttack CurrentAttack { get { return currentAttack; } set { currentAttack = value; } }
    public AnimationClip[] HitAnimations { get { return hitAnimations; } }
    public EnemyShieldCollider ShieldCollider { get { return shieldCollider; } }
    public EnemyWeaponCollider[] WeaponColliders { get { return weaponColliders; } }
    
    public float PlayerDistance { get { return playerDistance; } } 
    public float AggroRange { get { return aggroRange; } }
    public bool CanCombo { get { return canCombo; } set { canCombo = value; } }
    public bool Busy { get { return busy; } set { busy = value; } }
    public float RotateSpeed { get { return rotateSpeed; } }

    void Awake()
    {
        //init states
        if(stateFactoryType == 1)
        {
            stateFactory = new StateFactory_HUMANOID_SB(this);
        }
        else if(stateFactoryType == 2)
        {
            stateFactory = new WIZARD_StateFactory(this);
        }
        else
        {
            stateFactory = new GRUNT_StateFactory(this);
        }

        //init general
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();

        playerDistance = 1000f;
        currentState = stateFactory.Idle();

        shieldCollider = GetComponentInChildren<EnemyShieldCollider>();

        busy = false;

        //init attacks
        foreach(EnemyAttack attack in AttackList)
        {
            attack.enabled = true; //ensures attacks can be used
        }
        
    }
    private void Start()
    {
        if(weaponColliders == null) //automatically sets weapon colliders if not specified in editor
        {
            weaponColliders = GetComponentsInChildren<EnemyWeaponCollider>();
        }
    }

    void Update()
    {
        currentState.UpdateStates();
        currentState.CheckSwitchStates();

        animator.SetFloat("xVel", agent.velocity.x);
        //animator.SetFloat("zVel", agent.velocity.z);

        //update player distance
        playerDistance = Vector3.Distance(player.position, transform.position); 

        //DEBUGGING
        rootStateText.text = currentState.ToString();
        if(currentState.CurrentSubState != null)
        {
            level1StateText.text = currentState.CurrentSubState.ToString();
        }
        else
        {
            level1StateText.text = "no state";
        }
        if(currentState.CurrentSubState.CurrentSubState != null)
        {
            level2StateText.text = currentState.CurrentSubState.CurrentSubState.ToString();
        }
        else
        {
            level2StateText.text = "no state";
        }

    }
}
