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

    private void Update()
    {
        if (target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        } else
        {
            transform.position += transform.position.normalized * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Fires the missile to the new Target until timer runs out
    /// </summary>
    /// <param name="newTarget"></param>
    // Abstraction
    public void Fire(Transform newTarget)
    {
        damage = 1.5f;
        target = newTarget;
        Destroy(gameObject, aliveTimer);
    }
}
