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
    protected PlayerController playerController;

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
    // Abstraction
    protected void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        // If it isn't game over, do two things:
        // 1. If the Enemy collides with the Player, damages the Player and destroy self
        // 2. If the Enemy collides with the Laser, damages the Enemy
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (!playerController.gameOver)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerController.Damage(damage);
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Laser"))
            {
                Damage(playerController.laserDamage);
            }
        }
    }
}
