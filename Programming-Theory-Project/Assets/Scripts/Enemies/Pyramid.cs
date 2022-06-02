using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has medium health and speed, and high damage
/// </summary>
// Inheritance
public class Pyramid : Enemy
{
    // Polymorphism
    void Start()
    {
        health = 3;
        speed = 0.7f;
        damage = 1;
    }

    void Update()
    {
        Move();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
