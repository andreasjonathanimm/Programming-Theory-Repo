using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has medium health, medium damage, and increasing speed by health remaining
/// </summary>
// Inheritance
public class Prism : Enemy
{
    // Polymorphism
    void Start()
    {
        health = 3;
        damage = 0.7f;
    }

    void Update()
    {
        Move();
        speed = 1.5f / health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
