using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT NEEDS TO BE IMPROVED - TAKE INSPIRATION FROM ENEMY ATTACK FUNCTIONALLITY
//e.g. each weapon has an attack list, attacks are a scriptable object

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : Item
{
    public GameObject prefab; //weapon object prefab
    public WeaponType weaponType; 
    public AnimatorOverrideController animatorOverride;

    //determines the length of each attack
    public float lightAttackSpeed1; 
    public float lightAttackSpeed2;
    public float lightAttackSpeed3;

    public int damage;

    public void Awake()
    {
        
    }

    public void LightAttack1(Enemy enemy)
    {
        enemy.DealDamage(damage);
        enemy.LowerPosture(1);
    }
    public void LightAttack2(Enemy enemy)
    {
        enemy.DealDamage((int)(damage * 1.5));
        enemy.LowerPosture(2);
    }

    public void Finish(Enemy enemy)
    {
        enemy.DealDamage(1000); //change finish logic to have different anim pairs for player and enemy
    }
}

public enum WeaponType 
{ 
    Sword1H, 
    Sword2H
}
