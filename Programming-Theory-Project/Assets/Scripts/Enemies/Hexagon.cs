using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has somewhat slow speed, high health, low damage, and spawns minions during activity and upon death
/// </summary>
// Inheritance
public class Hexagon : Enemy
{
    // Encapsulation
    [SerializeField] private GameObject minion;
    // Polymorphism
    private void Start()
    {
        health = 6;
        speed = 0.5f;
        damage = 0.2f;
        InvokeRepeating("SpawnMinions(minion, 2)", 1, 2);
    }

    private void Update()
    {
        Move();
        if(health <= 0)
        {
            SpawnMinions(minion, 2);
            Destroy(gameObject);
        }
    }
}