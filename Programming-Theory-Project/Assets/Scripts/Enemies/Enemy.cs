using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class from which every Enemy script derives
/// </summary>
public class Enemy : MonoBehaviour
{
    // Encapsulation
    protected float health;
    protected float damage;
    protected float speed;
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

    /// <summary>
    /// Damages the Enemy
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Returns the Enemy damage
    /// </summary>
    /// <returns></returns>
    public float GetDamage()
    {
        return damage;
    }
}
