using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has somewhat normal speed, slighly higher health, and increasing damage by enemies remaining
/// </summary>
// Inheritance
public class Cube : Enemy
{
    // Polymorphism
    private void Start()
    {
        health = 4;
        speed = 0.75f;
    }

    private void Update()
    {
        Move();
        damage = 0.25f * GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
