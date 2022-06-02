using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns Objects in random position per waves
/// </summary>
public class SpawnManager : MonoBehaviour
{
    // Encapsulation
    [SerializeField] private GameObject[] laserPrefabs;
    [SerializeField] private GameObject[] enemyPrefabs;
    private float spawnRange = 20;
    private int waveCount = 1;

    private void Start()
    {
        SpawnLaserPowerUp();
        SpawnEnemies(waveCount);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            SpawnLaserPowerUp();
            waveCount++;
            SpawnEnemies(waveCount);
        }
    }
    /// <summary>
    /// Generates random position of X and Z within the spawn distance
    /// </summary>
    /// <param name="spawnDistance"></param>
    /// <returns>A Vector3 position</returns>
    // Abstraction
    private Vector3 GenerateRandomPosition(float spawnDistance)
    {
        
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

    /// <summary>
    /// Spawns a random LaserType in range of more than 2 distance of Player
    /// </summary>
    // Abstraction
    private void SpawnLaserPowerUp()
    {
        int laserTypeIndex = Random.Range(0, laserPrefabs.Length);
        Instantiate(laserPrefabs[laserTypeIndex], GenerateRandomPosition(2), laserPrefabs[laserTypeIndex].transform.rotation);
    }

    /// <summary>
    /// Spawns a random Enemy in range of more than 8 distance of Player
    /// </summary>
    // Abstraction
    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateRandomPosition(8), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}
