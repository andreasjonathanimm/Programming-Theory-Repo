using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Missile Behaviour
/// </summary>
public class MissileBehaviour : MonoBehaviour
{
    // Encapsulation
    private Transform target;
    private float speed = 2;
    public float damage { get; private set; }
    private float aliveTimer = 8;
    private bool isHoming;

    private void Update()
    {
        if (isHoming && target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    /// <summary>
    /// Fires the missile to the new Target until timer runs out
    /// </summary>
    /// <param name="newTarget"></param>
    // Abstraction
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        isHoming = true;
        Destroy(gameObject, aliveTimer);
    }
}
