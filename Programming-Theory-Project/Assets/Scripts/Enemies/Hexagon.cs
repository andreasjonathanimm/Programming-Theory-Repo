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
    private bool hasSpawn = true;
    // Polymorphism
    private void Start()
    {
        health = 6;
        speed = 0.5f;
        damage = 0.2f;
        InvokeRepeating("SpawnMinions", 1, 3);
    }

    private void Update()
    {
        Move();
        if(health <= 3 && hasSpawn)
        {
            SpawnMinions(minion, 3, 3);
            hasSpawn = false;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SpawnMinions()
    {
        base.SpawnMinions(minion, 2, 1);
    }
}
