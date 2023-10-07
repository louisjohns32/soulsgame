using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract base state which all enemy states inherrit from
public abstract class EnemyBaseState
{
    protected int level = 0; //level of state - root = 0, their children = 1, their children = 2 etc.
    protected EnemyStateManager stateManager; //reference to stateManager
    protected EnemyStateFactory stateFactory; //reference to stateFactory

    protected EnemyBaseState currentSubState; //reference to current substate
    protected EnemyBaseState currentSuperState; //reference to superstate

    //getters for sub and super states
    public EnemyBaseState CurrentSubState { get { return currentSubState; } }
    public EnemyBaseState CurrentSuperState { get { return currentSuperState; } }

    public EnemyBaseState(EnemyStateManager stateManager, EnemyStateFactory stateFactory)
    { //comstructor sets references to state manager and factory
        this.stateManager = stateManager;
        this.stateFactory = stateFactory;
    }

    public abstract void EnterState(); //called once upon entry of state

    public abstract void CheckSwitchStates(); //called every update - contains logic to check if state should be changed

    public abstract void UpdateState(); //called every update - contains general code which should be called every tick

    public abstract void ExitState();   //called once upon state exit

    public abstract void WeaponCollide(Collider collider); //called when weapon collider collides with player

    public abstract void InitializeSubState(); //initialises the sub state on state enter
    
    public void UpdateStates() //calls update methods of itself and its substate
    {
        UpdateState();
        CheckSwitchStates();
        if (currentSubState != null)
        {
            currentSubState.UpdateStates();
            if (currentSubState != null) //this has to be checked again as the substate may have changed in the meantime
            {
                currentSubState.CheckSwitchStates();
            }
        }
    }
    public void ChangeState(EnemyBaseState state, EnemyBaseState subState = null)
    {
        if(state.level == level)//validates that the state is of the correct level
        {
            ExitState(); //exit state function of self is called

            state.EnterState(); //enter state of new state is called
            if (subState == null) //if no substate is specified, call the new states initialize substate
            {
                state.InitializeSubState();
            }
            else
            {
                state.SetSubState(subState); //sets substate of new state to the one specified
            }


            if (level == 0)
            { //root state is changed
                stateManager.CurrentState = state;
            }
            else if (currentSuperState != null)
            { //changes the parents sub state reference to the new state. (removing all references to this object, rendering it as obsolete)
                currentSuperState.SetSubState(state);
            }
        } 
        

        
    }

    public void CallWeaponCollide(Collider collider) //calls own and all substates' weaponcollide()
    {
        WeaponCollide(collider);
        if(currentSubState != null)
        {
            currentSubState.CallWeaponCollide(collider);
        }
    }
    
    
    public virtual void TakeDamage(float damage)
    {
        if (level == 0) //should only be called once within root state.
        {
            stateManager.Enemy.health -= damage; //health is reduced
            //change level 1 state to HitState, if it isn't already
            if (stateManager.getCurrentState(level: 1).GetType() != typeof(EnemyHitState))
            {
                stateManager.getCurrentState(level: 1).ChangeState(stateFactory.HitState()); 
            }
        }
        if(currentSubState != null) //calls take damage of substate
        {
            currentSubState.TakeDamage(damage); 
        }
    }

    protected void SetSuperState(EnemyBaseState superState) 
    {
        currentSuperState = superState;
    }

    protected void SetSubState(EnemyBaseState subState, bool runEnter = true)
    {
        currentSubState = subState;
        if (subState != null)
        {
            subState.SetSuperState(this); //sets substates reference to this state
            subState.InitializeSubState(); //initializes substate's substate
            if (runEnter) //runEnter can be passed in as false to prevent the substate's EnterState() being called
            {
                subState.EnterState();
            }
        }

    }
}
