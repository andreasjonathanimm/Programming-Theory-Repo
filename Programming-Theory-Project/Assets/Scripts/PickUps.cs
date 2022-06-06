using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Object that can be picked up by the Player and is spinning!
/// </summary>
public class PickUps : MonoBehaviour
{
    protected float rotationSpeed = 100;

    protected void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
