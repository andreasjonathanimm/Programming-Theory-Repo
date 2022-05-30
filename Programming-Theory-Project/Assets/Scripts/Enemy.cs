using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Encapsulation
    [SerializeField] protected float speed;
    [SerializeField] protected bool isBoss;

    protected SpawnManager spawnManager;
    protected GameObject player;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    protected void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
