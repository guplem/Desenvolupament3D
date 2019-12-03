using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager pm = other.GetComponent<PlayerManager>();
        if (pm == null) return;
        pm.coins++;
        Destroy(gameObject);
    }
}
