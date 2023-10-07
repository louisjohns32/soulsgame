using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollider : MonoBehaviour
{
    EnemyStateManager stateManager;

    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
