using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateManager : MonoBehaviour
{
    //states
    PlayerBaseState currentState;
    PlayerStateFactory stateFactory;

    //TEMP public variables to be changed and given getters/setters 
    public CinemachineVirtualCamera focusCam;
    public GameObject focusedEnemy;

    //GENERAL
    Camera mainCam;
    Animator playerAnimator;
    PlayeEquipsManager equipsManager;


    //COMBAT
    [SerializeField] float range = 10f;

    GameObject weaponObj;
    WeaponCollider weaponCollider;

    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Transform handTransform;
    [SerializeField] Transform armourTransform;

    Armour[] equippedArmours;
    GameObject[] equippedArmoursObj;

    private List<GameObject> enemiesInRange;

    HealthBar playerHealthBar;
    StaminaBar playerStamBar;


    //variables related to combo
    LightAttacks lastAttack;
    float attackStartTime;
    Vector3 attackDirectionVector;
    float attackSpeed;
    bool attackAgain;

    //player status variables
    [SerializeField] float maxHealth = 100f;
    float health;
    float stamina;
    float lastDrained; //Time stamina was last drained
    [SerializeField] float recovCD = 2f; //Time until stamina starts regenerating
    [SerializeField] float recovRate = 50f; //Rate of stamina regeneration (points/s)

    private float focusAngle;
    int clicks;

    bool isAttacking;

    bool finishing;

    //MOVEMENT
    CharacterController charController;

    //gravity
    Transform groundTracker;
    [SerializeField] LayerMask groundMask; // TODO get rid of serialize field and init in awake function

    float speed = 3f;
    float rotationSmoothing = 0.1f;
    public float rotationSmoothingVel;

    float gravity = -9.8f;

    [SerializeField] float rollSpeed = 5f;
    [SerializeField] float rollTime = 0.5f;
    [SerializeField] float rollCost = 40f;

    float rollCD = 1f;

    float forwardAngle;

    float yVel;

    float xMovement, zMovement;
    Vector3 directionVector, moveDirection;

    bool isGrounded;

    float rollTimer;

    //GETTERS & SETTERS
    //state
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    
    //general
    public Camera MainCam { get { return mainCam; } }
    public Animator PlayerAnimator { get { return playerAnimator; } }

    //combat
    public float Range { get { return range; } set { range = value; } }
    public LightAttacks LastAttack {  get { return lastAttack; } set { lastAttack = value; } }
    public WeaponCollider WeaponCollider { get { return weaponCollider; } set { weaponCollider = value; } } 
    public Weapon EquippedWeapon { get { return equippedWeapon; } set { equippedWeapon = value; } }
    public float AttackStartTime { get { return attackStartTime; } set { attackStartTime = value; } }
    public Vector3 AttackDirectionVector { get { return attackDirectionVector; } set { attackDirectionVector = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public bool AttackAgain {  get { return attackAgain; } set { attackAgain = value; } }   
    public float Health { get { return health; } set { health = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float Stamina { get { return stamina; }  set{ stamina = value; } }
    public float LastDrained { get { return lastDrained; } } 
    public float RecovCD { get { return recovCD; }  }
    public float RecovRate { get { return recovRate; }  }
    public HealthBar PlayerHealthBar { get { return playerHealthBar; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public List<GameObject> EnemiesInRange { get { return enemiesInRange; } set { enemiesInRange = value; } }

    //movement
    public CharacterController CharController {  get { return charController; } }
    public Transform GroundTracker { get { return groundTracker; } }
    public LayerMask GroundMask { get { return groundMask; } }
    public float ForwardAngle {  get { return forwardAngle; } set { forwardAngle = value; } }
    public float Speed { get { return speed; } }
    public float RotationSmoothing { get { return rotationSmoothing; } }
    public float Gravity {  get { return gravity; } }
    public float RollSpeed {  get { return rollSpeed; } }
    public float RollTime { get { return rollTime; } }
    public float RollCD {  get { return rollCD; } }
    public float RollAngle {  get { return RollAngle; } }
    public float RollCost { get { return rollCost; } }

    // Start is called before the first frame update
    void Awake()
    {
        //init states
        stateFactory = new PlayerStateFactory(this);

        currentState = stateFactory.Grounded();

        //init general
        mainCam = Camera.main;
        playerAnimator = GetComponent<Animator>();
        equipsManager = GetComponent<PlayeEquipsManager>();

        //init combat
        health = maxHealth;
        stamina = 100;
        playerHealthBar = GetComponentInChildren<HealthBar>();
        playerHealthBar.SetMaxHealth(maxHealth);
        playerStamBar = GetComponentInChildren<StaminaBar>();
        playerStamBar.SetMaxStam(100);

        //init movement
        charController = GetComponent<CharacterController>();
        groundTracker = GameObject.FindGameObjectWithTag("GroundTracker").transform;
    }

    private void Start()
    {
        equippedWeapon = (Weapon)equipsManager.GetEquipped(0); //sets equipped weapon
        if (equippedWeapon.animatorOverride != null)
        {
            playerAnimator.runtimeAnimatorController = equippedWeapon.animatorOverride; //sets animator override if one exists
        }
        weaponObj = Instantiate(equippedWeapon.prefab, handTransform); //initializes weapon object
        weaponCollider = weaponObj.GetComponent<WeaponCollider>(); //init weapon collider
        weaponCollider.DisableCollision();

        equippedArmours = new Armour[7];
        equippedArmoursObj = new GameObject[7];
        for(int i = 0; i < 7; i++)
        {
            equippedArmours[i] = (Armour)equipsManager.GetEquipped(i + 2);
            equippedArmoursObj[i] = Instantiate(equippedArmours[i].armourPrefab, armourTransform);
        }
        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateStates();
        CurrentState.CheckSwitchStates();
    }

    public void TakeDamage(float dmg) //reduces health and changes to hit state
    {
        currentState.CurrentSubState.CurrentSubState.ChangeState(stateFactory.HitState());
        health -= dmg;
        playerHealthBar.SetCurrentHealth(health);
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void DrainStamina(float amount)//reduces stamina 
    {
        stamina -= amount;
        lastDrained = Time.time;
        playerStamBar.SetCurrentStam(stamina);
    }
    public void RecoverStamina(float amount) //increases stamina
    {
        stamina += amount;
        playerStamBar.SetCurrentStam(stamina);
    }

    public void ChangeEquippedWeapon(Weapon weapon)
    {
        playerAnimator.CrossFade("ChangeWeapon", 0.2f);//plays animation
        equippedWeapon = weapon; 
        if (equippedWeapon.animatorOverride != null) //updates animator override
        {
            playerAnimator.runtimeAnimatorController = weapon.animatorOverride;
        }
        GameObject.Destroy(weaponObj);
        weaponObj = null;
        weaponObj = Instantiate(equippedWeapon.prefab, handTransform);//creates new weapon object 
        weaponCollider = weaponObj.GetComponent<WeaponCollider>();
        weaponCollider.DisableCollision();
    }
    public void ChangeEquippedArmour(Armour armour, int slotID)
    {
        //SET OBJECT ACTIVE BLAH BLAH BLAH call from PlayerEquipsManager
    }

}
