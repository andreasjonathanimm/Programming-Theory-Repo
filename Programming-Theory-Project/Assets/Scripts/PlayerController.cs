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
    private float shootSpeed = 0.5f;
    public float score { get; private set; }
    public float laserDamage { get; private set; }
    private float laserAlive = 0.25f;
    private float powerUpTime = 5;
    private bool isShooting = false;
    private bool isMissileReady = true;
    private bool hasPowerUp = false;
    private bool isHeal = true;
    private bool isOverHeal = false;
    public bool gameOver { get; private set; }

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private GameObject singleLaser;
    [SerializeField] private GameObject triLaser;
    [SerializeField] private GameObject spreadLaser;
    [SerializeField] private GameObject ABSLaser;

    [SerializeField] private GameObject powerUp_Indicator;
    [SerializeField] private GameObject missilePrefab;

    private LaserType currentLaser = LaserType.Single;
    private PowerUpType currentPowerUp;

    private GameManager gameManager;

    [SerializeField] private AudioClip laserClip;
    [SerializeField] private AudioClip windClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip shieldClip;
    [SerializeField] private AudioClip missileClip;
    [SerializeField] private AudioClip STRONKClip;
    [SerializeField] private AudioClip healClip;
    private AudioSource audioSource;
    private void Start()
    {
        gameOver = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameOver)
        {
            // Rotates horizontally
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            objectPosition = Input.mousePosition - objectPosition;
            float angle = Mathf.Atan2(objectPosition.y, objectPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);

            // Checks current LaserType and fires it
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                CheckLaserType();
                ShootLasers();
            }

            // PowerUp Check
            if(currentPowerUp == PowerUpType.Missile && isMissileReady)
            {
                StartCoroutine(ShootMissiles());
                isMissileReady = false;
            }

            if (currentPowerUp == PowerUpType.Health && isHeal)
            {
                StartCoroutine(Heal(0.3f));
                isHeal = false;
            }

            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (health > 3.14f && !isOverHeal)
        {
            StartCoroutine(OverHeal());
        }
        healthSlider.value = health * 100;
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
            laserDamage = 1.5f;
        }

        if (currentLaser == LaserType.Tri)
        {
            shootSpeed = 3;
            laserDamage = 0.5f;
        }

        if (currentLaser == LaserType.Spread)
        {
            shootSpeed = 5;
            laserDamage = 0.25f;
        }

        if (currentLaser == LaserType.ABSSpread)
        {
            shootSpeed = 10;
            laserDamage = 0.15f;
        }
        if (currentPowerUp == PowerUpType.STRONK_Laser)
        {
            laserDamage *= 3;
        }
    }

    /// <summary>
    /// Shoots Laser by the LaserType when currently not shooting
    /// </summary>
    // Abstraction
    private void ShootLasers()
    {
        if (isShooting) return;
        audioSource.PlayOneShot(laserClip, 0.8f);
        if (currentPowerUp == PowerUpType.STRONK_Laser) { audioSource.PlayOneShot(STRONKClip, 0.5f); }
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
        audioSource.PlayOneShot(missileClip, 0.8f);
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GameObject tmpMissile = Instantiate(missilePrefab, gameObject.transform.position, missilePrefab.transform.rotation);
            tmpMissile.GetComponent<MissileBehaviour>().Fire(enemy.transform);
        }
        yield return new WaitForSeconds(1);
        isMissileReady = true;
    }

    public IEnumerator Heal(float healingPoints)
    {
        health += healingPoints;
        audioSource.PlayOneShot(healClip, healingPoints + 0.1f);
        yield return new WaitForSeconds(1);
        isHeal = true;
    }

    IEnumerator OverHeal()
    {
        isOverHeal = true;
        health -= health/314;
        yield return new WaitForSeconds(0.1f);
        isOverHeal = false;
    }

    /// <summary>
    /// Damages the Player while not shielded
    /// </summary>
    /// <param name="damage"></param>
    // Abstraction
    public void Damage(float damage)
    {
        if (currentPowerUp == PowerUpType.Shield) { audioSource.PlayOneShot(shieldClip, 0.6f); Heal(damage/5); return; }
        health -= damage;
        audioSource.PlayOneShot(hurtClip, 1);
        if(health <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Adds up the score
    /// </summary>
    /// <param name="score"></param>
    // Abstraction
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
        hasPowerUp = true;
        powerUp_Indicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(powerUpTime);
        powerUp_Indicator.gameObject.SetActive(false);
        currentPowerUp = PowerUpType.None;
        hasPowerUp = false;
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    // Abstraction
    private void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        audioSource.Stop();
        audioSource.PlayOneShot(windClip, 1);
    }

    /// <summary>
    /// To return to the menu
    /// </summary>
    // Abstraction
    public void ReturnToMenu()
    {
        GameOver();
        if (gameManager != null) { gameManager.LoadMenu(); }
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
            StartCoroutine(PowerUpCooldown());
            Destroy(other.gameObject);
        }
    }
}
