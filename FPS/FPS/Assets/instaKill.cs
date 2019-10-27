using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instaKill : MonoBehaviour
{
    [SerializeField] private int damage = 1000;
    private void OnTriggerEnter(Collider other)
    {
        Health h = other.GetComponent<Health>();
        if (h == null) return;
        
        h.Hurt(damage);
    }
}
