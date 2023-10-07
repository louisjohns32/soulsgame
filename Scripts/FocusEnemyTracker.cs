using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEnemyTracker : MonoBehaviour
{
    public PlayerCombat player;
    public void UpdatePos()
    {
        Debug.Log(player.gameObject.transform.position);
        transform.position =  Vector3.Lerp(player.gameObject.transform.position, player.GetFocused().transform.position, 0.5f) ;
        transform.LookAt(player.GetFocused().transform.position);
    }
}
