using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Encapsulation
    [SerializeField] private GameObject[] laserPrefabs;

    private float spawnRange = 20;
    private void Start()
    {
        SpawnLaserPowerUp();
    }

    // Abstraction
    private Vector3 GenerateRandomPosition(float spawnDistance)
    {
        // Generates random position of X and Z within the game area
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange/2, -spawnRange/2);

        // Loops until the position is not in range of spawnDistance of Player (Center point)
        while (System.Math.Abs(spawnPosX) < spawnDistance || System.Math.Abs(spawnPosZ) < spawnDistance)
        {
            spawnPosX = Random.Range(spawnRange, -spawnRange);
            spawnPosZ = Random.Range(spawnRange/2, -spawnRange/2);
        }

        // Declares the random position and returns it
        Vector3 randomPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);
        return randomPos;
    }

    // Abstraction
    private void SpawnLaserPowerUp()
    {
        // Spawns a random LaserType in range of more than 2 distance of player
        int laserTypeIndex = Random.Range(0, laserPrefabs.Length);
        Instantiate(laserPrefabs[laserTypeIndex], GenerateRandomPosition(2), laserPrefabs[laserTypeIndex].transform.rotation);
    }
}
