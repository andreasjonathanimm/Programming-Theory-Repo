using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class from which every Enemy script derives
/// </summary>
public class Enemy : MonoBehaviour
{
    // Encapsulation
    protected int health;
    protected float speed = 1;
    protected GameObject player;

    /// <summary>
    /// Move towards the Player as fast as speed with Translate and LookAt
    /// </summary>
    // Abstraction
    protected void Move()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// Spawns Minions by times 
    /// </summary>
    /// <param name="minion"></param>
    /// <param name="times"></param>
    // Abstraction
    protected void SpawnMinions(GameObject minion, int times)
    {
        for (int i = 0; i < times; i++) { Instantiate(minion, gameObject.transform.position, minion.transform.rotation); }
    }

    protected void GetDamage(int damage)
    {
        health -= damage;
    }
}
