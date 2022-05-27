using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Encapsulation
    [SerializeField] private float rotationSpeed = 45.0f;
    [SerializeField] private float shootSpeed = 2;
    [SerializeField] private float laserAlive = 0.25f;
    [SerializeField] private GameObject singleLaser;
    [SerializeField] private GameObject triLaser;
    [SerializeField] private GameObject spreadLaser;
    [SerializeField] private GameObject ABSLaser;
    private float horizontalInput;
    private bool isShooting = false;
    [SerializeField] private LaserType currentLaser = LaserType.Single;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);

        CheckLaserType();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootLasers();
        }
    }

    // Abstraction
    private void CheckLaserType()
    {
        if (currentLaser == LaserType.Single)
        {
            shootSpeed = 2;
        }

        if (currentLaser == LaserType.Tri)
        {
            shootSpeed = 2.6f;
        }

        if (currentLaser == LaserType.Spread)
        {
            shootSpeed = 3;
        }

        if (currentLaser == LaserType.ABSSpread)
        {
            shootSpeed = 5;
        }
    }

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

    // Abstraction
    IEnumerator ShootOneShot(GameObject laser)
    {
        laser.SetActive(true);
        yield return new WaitForSeconds(laserAlive);
        laser.SetActive(false);
        yield return new WaitForSeconds(1/shootSpeed);
        isShooting = false;
    }
}