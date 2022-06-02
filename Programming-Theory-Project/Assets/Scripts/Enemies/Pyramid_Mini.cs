using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Minion that has low health, great speed, and low damage
/// </summary>
// Inheritance
public class Pyramid_Mini : Enemy
{
    // Polymorphism
    void Start()
    {
        health = 0.5f;
        speed = 1.2f;
        damage = 0.1f;
    }

    void Update()
    {
        Move();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
