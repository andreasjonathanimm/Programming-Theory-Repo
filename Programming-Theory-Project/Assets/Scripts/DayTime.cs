using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a DayTime cycle
/// </summary>
public class DayTime : MonoBehaviour
{
    // Encapsulation
    private float rotationSpeed;
    public float dayLength;

    private void Update()
    {
        rotationSpeed = Time.deltaTime / dayLength;
        transform.Rotate(transform.right, rotationSpeed);
    }
}
