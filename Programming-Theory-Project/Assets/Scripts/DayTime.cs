using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime : MonoBehaviour
{
    private float rotationSpeed;
    public float dayLength;

    private void Update()
    {
        rotationSpeed = Time.deltaTime / dayLength;
        transform.Rotate(transform.right, rotationSpeed);
    }
}
