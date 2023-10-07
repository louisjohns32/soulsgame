using UnityEngine;
public abstract class PlayerBaseState
{
    protected bool isRootState = false;
    protected PlayerStateManager stateManager; //reference to stateManager
    protected PlayerStateFactory stateFactory; //reference to stateFactory

    protected PlayerBaseState currentSuperState; //reference to superstate
    protected PlayerBaseState currentSubState; //reference to substate

    public bool canChangeWeapon = true;

    public PlayerBaseState CurrentSuperState { get { return currentSuperState; } }
    public PlayerBaseState CurrentSubState { get { return currentSubState; } }
    public PlayerBaseState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
    {
        this.stateManager = stateManager;
        this.stateFactory = stateFactory;
    }

    public abstract void EnterState(); //called once on state entry

    public abstract void ExitState(); //called once on state exit

    public abstract void UpdateState(); //called every update

    public abstract void CheckSwitchStates(); //called every update - contains logic to check if state should be changed

    public abstract void InitializeSubState(); //called on state entry

    public void UpdateStates()  //calls update state and checkswitch state of self and all substates
    { 
        UpdateState();
        CheckSwitchStates();
        if(currentSubState != null)
        {
            currentSubState.UpdateStates();
            if(currentSubState != null)
            {
                currentSubState.CheckSwitchStates();
            }
            
        }
    }

    public void ChangeState(PlayerBaseState state) //handles changing of state
    { 
        ExitState(); 
        
        state.EnterState();

        if (isRootState)
        {
            stateManager.CurrentState = state;
        }else if(currentSuperState != null)
        {
            currentSuperState.SetSubState(state);
        }
    }

    public virtual void TakeDamage(int damage)//lowers health
    {
        stateManager.Health -= damage; //TODO add damage mitigation
    }
    public virtual void DealDamage(Collider enemy) //handles dealing damage to enemy
    {
        if(currentSubState != null)
        {
            currentSubState.DealDamage(enemy);  
        }
    }
    public virtual void BlockAttack()
    {
        Debug.LogError("Player is not currently attacking yet BlockAttack was called"); //by default shouldn't be called. Some states have overrides
    }

    public bool ChangeWeapon(Weapon weapon)
    {
        if (!canChangeWeapon) //validates if weapon can currently be changed
        {
            return false;
        }
        else
        {
            if(currentSubState != null)
            {
                return currentSubState.ChangeWeapon(weapon);
            }
            else
            {
                stateManager.ChangeEquippedWeapon(weapon);
                return true;
            }
        }
    }

    protected void SetSuperState(PlayerBaseState superState) 
    { 
        currentSuperState = superState;
    }

    protected void SetSubState(PlayerBaseState subState, bool runEnter = false) 
    {
        currentSubState = subState;
        if(subState != null)
        {
            subState.SetSuperState(this);
            if (runEnter)
            {
                subState.EnterState();
            }
        }
        
    }
}
