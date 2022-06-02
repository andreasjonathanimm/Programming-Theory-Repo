using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Player rotation and shooting
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Encapsulation
    private float health = 3.14f;
    private float rotationSpeed = 100;
    private float shootSpeed = 0.5f;
    private float laserDamage = 1;
    private float laserAlive = 0.25f;
    private float horizontalInput;
    private bool isShooting = false;
    private bool gameOver = false;

    [SerializeField] private GameObject singleLaser;
    [SerializeField] private GameObject triLaser;
    [SerializeField] private GameObject spreadLaser;
    [SerializeField] private GameObject ABSLaser;

    private LaserType currentLaser = LaserType.Single;

    private void Update()
    {
        if (!gameOver)
        {
            // Rotates horizontally
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);

            // Checks current LaserType and fires it
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckLaserType();
                ShootLasers();
            }
        }
    }

    /// <summary>
    /// Checks the current LaserType and change the shooting speed
    /// </summary>
    // Abstraction
    private void CheckLaserType()
    {

        if (currentLaser == LaserType.Single)
        {
            shootSpeed = 2;
            laserDamage = 1;
        }

        if (currentLaser == LaserType.Tri)
        {
            shootSpeed = 3;
            laserDamage = 0.75f;
        }

        if (currentLaser == LaserType.Spread)
        {
            shootSpeed = 5;
            laserDamage = 0.4f;
        }

        if (currentLaser == LaserType.ABSSpread)
        {
            shootSpeed = 10;
            laserDamage = 0.2f;
        }
    }

    /// <summary>
    /// Shoots Laser by the LaserType when currently not shooting
    /// </summary>
    // Abstraction
    private void ShootLasers()
    {
        if (isShooting) return;
        if (currentLaser == LaserType.Single)
        {
            isShooting = true;
            StartCoroutine(ShootOneShot(singleLaser));
        }

        if (currentLaser == LaserType.Tri)
        {
            isShooting = true;
            StartCoroutine(ShootOneShot(triLaser));
        }

        if (currentLaser == LaserType.Spread)
        {
            isShooting = true;
            StartCoroutine(ShootOneShot(spreadLaser));
        }

        if (currentLaser == LaserType.ABSSpread)
        {
            isShooting = true;
            StartCoroutine(ShootOneShot(ABSLaser));
        }
    }

    /// <summary>
    /// Damages the Player
    /// </summary>
    /// <param name="damage"></param>
    // Abstraction
    private void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameOver = true;
        }
    }

    /// <summary>
    /// Adds a cooldown to the shooting lasers
    /// </summary>
    // Abstraction
    IEnumerator ShootOneShot(GameObject laser)
    { 
        laser.SetActive(true);
        yield return new WaitForSeconds(laserAlive);
        laser.SetActive(false);
        yield return new WaitForSeconds(1/shootSpeed);
        isShooting = false;
    }

    // Abstraction
    private void OnTriggerEnter(Collider other)
    {
        // If the laser collides with a LaserType, get its type and destroys it
        if (other.CompareTag("LaserType"))
        {
            currentLaser = other.gameObject.GetComponent<Laser>().laserType;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !gameOver) {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            if (gameObject.CompareTag("Laser"))
            {
                enemyScript.Damage(laserDamage);
            }
            if (gameObject.CompareTag("Player"))
            {
                Damage(enemyScript.GetDamage());
                Destroy(collision.gameObject);
            }
        }
    }
}
