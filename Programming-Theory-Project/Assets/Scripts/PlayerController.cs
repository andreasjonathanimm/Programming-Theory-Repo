using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls the Player rotation and shooting
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Encapsulation
    private float health = 3.14f;
    private float rotationSpeed = 100;
    private float shootSpeed = 0.5f;
    private float score = 0;
    public float laserDamage { get; private set; }
    private float laserAlive = 0.25f;
    private float powerUpTime = 5;
    private float horizontalInput;
    private bool isShooting = false;
    private bool isMissileReady = true;
    private bool hasPowerUp = false;
    public bool gameOver { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject singleLaser;
    [SerializeField] private GameObject triLaser;
    [SerializeField] private GameObject spreadLaser;
    [SerializeField] private GameObject ABSLaser;

    [SerializeField] private GameObject powerUp_Indicator;
    [SerializeField] private GameObject missilePrefab;

    private LaserType currentLaser = LaserType.Single;
    private PowerUpType currentPowerUp;

    private void Start()
    {
        gameOver = false;
    }

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

            if(currentPowerUp == PowerUpType.Missile && isMissileReady)
            {
                StartCoroutine(ShootMissiles());
                isMissileReady = false;
            }

            if (currentPowerUp == PowerUpType.INFINITE_Laser)
            {
                laserAlive = 5;
            } else
            {
                laserAlive = 0.25f;
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

    IEnumerator ShootMissiles()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GameObject tmpMissile = Instantiate(missilePrefab, gameObject.transform.position, missilePrefab.transform.rotation);
            tmpMissile.GetComponent<MissileBehaviour>().Fire(enemy.transform);
        }
        yield return new WaitForSeconds(1);
        isMissileReady = true;
    }

    /// <summary>
    /// Damages the Player while not shielded
    /// </summary>
    /// <param name="damage"></param>
    // Abstraction
    public void Damage(float damage)
    {
        if (currentPowerUp == PowerUpType.Shield) { return; }
        health -= damage;
        if(health <= 0)
        {
            gameOver = true;
        }
    }

    public void AddScore(float score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }

    /// <summary>
    /// Adds a cooldown to the shooting lasers
    /// </summary>
    // Abstraction
    IEnumerator ShootOneShot(GameObject laser)
    { 
        // Activates the laser for a certain duration of time, then turns it off, cooldowns it, and reset
        laser.SetActive(true);
        yield return new WaitForSeconds(laserAlive);
        laser.SetActive(false);
        yield return new WaitForSeconds(1/shootSpeed);
        isShooting = false;
    }

    /// <summary>
    /// Adds a cooldown to Power Up
    /// </summary>
    // Abstraction
    IEnumerator PowerUpCooldown()
    {
        powerUp_Indicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(powerUpTime);
        powerUp_Indicator.gameObject.SetActive(false);
        currentPowerUp = PowerUpType.None;
        hasPowerUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the laser collides with a LaserType, get its type and destroys it
        if (other.CompareTag("LaserType"))
        {
            currentLaser = other.gameObject.GetComponent<Laser>().laserType;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUp") && !hasPowerUp)
        {
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCooldown());
        }
    }
}
