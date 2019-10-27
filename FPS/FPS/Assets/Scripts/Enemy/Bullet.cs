using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Bullet : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;
    [SerializeField] private int damage;
    
    //private Vector3 targetPosition;
    // Update is called once per frame
    void Update()
    {
        if (direction == Vector3.zero) return;
        
        transform.position += direction.normalized * speed;
    }

    [SerializeField] private float speed;

    public void SetTarget(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}
