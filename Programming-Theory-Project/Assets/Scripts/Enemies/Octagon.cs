using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has slowest speed, very high health, high damage decreasing by health, and drops minions upon death
/// </summary>
// Inheritance
public class Octagon : Enemy
{
    // Encapsulation
    [SerializeField] private GameObject minion;
    // Polymorphism
    private void Start()
    {
        speed = 0.375f;
        health = 8;
    }

    private void Update()
    {
        Move();
        damage = health / 4;
        if (health <= 0)
        {
            SpawnMinions(minion, 3);
            Destroy(gameObject);
        }
    }
}
