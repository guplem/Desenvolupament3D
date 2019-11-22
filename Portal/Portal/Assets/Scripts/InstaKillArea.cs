using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstaKillArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
            Destroy(other.gameObject);
        
        else if (other.gameObject.CompareTag("Player"))
            GameManager.Instance.Kill();
    }
}
