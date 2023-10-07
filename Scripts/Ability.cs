using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    GameObject item;
    Component[] components;

    void Start()
    {
        try { item = transform.GetChild(0).gameObject; }
        catch { item = null; }
    }

    void Update()
    {
        
    }

    public void Cast()
    {
        components = item.GetComponents<Component>();
        //components[components.Length - 1].
    }
}
