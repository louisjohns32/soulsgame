using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : ScriptableObject
{
    GameObject player;
    GameObject targetEnemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void oneHandedSwordLightAttack_01()
    {
        targetEnemy = player.GetComponent<PlayerCombat>().GetFocused();

        //calc damage using enemy mit, and player stats, allow for crit
        //player run animation
    }
    public void oneHandedSwordLightAttack_02()
    {
        targetEnemy = player.GetComponent<PlayerCombat>().GetFocused();

    }


}
