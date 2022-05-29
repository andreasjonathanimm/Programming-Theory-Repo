using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaserType { Single, Tri, Spread, ABSSpread }

public class Laser : MonoBehaviour
{
    public LaserType laserType;

    // Encapsulation
    private float rotationSpeed = 100;

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
