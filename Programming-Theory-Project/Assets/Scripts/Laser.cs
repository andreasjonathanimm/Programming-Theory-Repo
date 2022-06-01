using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types of Lasers:
/// <list type="number">
/// <item>Single</item>
/// <item>Tri</item>
/// <item>Spread</item>
/// <item>ABSOLUTE Spread</item>
/// </list>
/// </summary>
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
