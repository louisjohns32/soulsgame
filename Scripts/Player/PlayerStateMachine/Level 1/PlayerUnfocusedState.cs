using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnfocusedState : PlayerBaseState
{
    bool _qUp;
    public PlayerUnfocusedState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) : base(stateManager, stateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //ensures that the press of Q is different from the one used to enter state
        if (Input.GetKeyUp(KeyCode.Q)) 
        {
            _qUp = true;

        }
        //checks for press of Q 
        else if (Input.GetKeyDown(KeyCode.Q) && _qUp)
        {
            UpdateEnemiesInRange();
            if(stateManager.EnemiesInRange.Count > 0) //validates list is not empty
            {
                ChangeState(stateFactory.Focused());
            }
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //TODO implement sprinting
            SetSubState(stateFactory.Walking());
        }
        else
        {
            SetSubState(stateFactory.Idle());
        }
    }

    public override void UpdateState()
    {
        stateManager.ForwardAngle = stateManager.MainCam.transform.eulerAngles.y;
    }

    private void UpdateEnemiesInRange()
    {
        List<GameObject> enemiesInRange = new List<GameObject>(); //temporary list
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")) //loops through each enemy in scene
        {
            if ((obj.transform.position - stateManager.transform.position).sqrMagnitude 
                <= stateManager.Range * stateManager.Range) enemiesInRange.Add(obj); //adds to list if in range
        }
        stateManager.EnemiesInRange = enemiesInRange; //sets stateManager enemiesInRange list
    }
}
