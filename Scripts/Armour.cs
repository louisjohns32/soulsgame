using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Armour")]
public class Armour : Item
{
    public GameObject armourPrefab; //armour prefab object
    public int slot; //the inventory slot the armour type belongs in (on character sheet)
}

