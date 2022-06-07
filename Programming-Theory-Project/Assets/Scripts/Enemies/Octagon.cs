using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has slowest speed, very high health, high damage decreasing by health, and drops minions upon half health
/// </summary>
// Inheritance
public class Octagon : Enemy
{
    // Encapsulation
    [SerializeField] private GameObject minion;
    private bool hasSpawn = true;
    // Polymorphism
    private void Start()
    {
        speed = 0.4f;
        health = 8;
    }

    private void Update()
    {
        Move();
        damage = health / 4;
        if (health <= 4 && hasSpawn)
        {
            SpawnMinions(minion, 8, 4);
            hasSpawn = false;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
