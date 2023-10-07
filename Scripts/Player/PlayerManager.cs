using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public CinemachineVirtualCamera focusCam;
    public Animator animator;

    GameObject[] equipped;

    bool isBusy;
    
    
    
    void Start()
    {
        equipped = new GameObject[8];
    }

    
    void Update()
    {
        
    }

    public GameObject GetEquipped(int slot)
    {
        return equipped[slot];
    }

    public void Focus(bool focus)
    {
        animator.SetBool("focused", focus);
        if (focus)
        {
            focusCam.gameObject.SetActive(true);
        }
        else
        {
            focusCam.gameObject.SetActive(false);
        }
    }

    public bool getBusy()
    {
        return isBusy;
    }
    public void setBusy(bool x)
    {
        isBusy = x;
    }
}
