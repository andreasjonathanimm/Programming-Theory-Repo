using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Enemy that has less speed and more health
/// </summary>
// Inheritance
public class Octagon : Enemy
{
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
        if (health <= 0)
        {
            SpawnMinions(minion, 3);
            Destroy(gameObject);
        }
    }
}
