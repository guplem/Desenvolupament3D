using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform destinationPosition;
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float speed = 1f;
    
    [SerializeField] private int pointsToOpen;
    [SerializeField] private bool needKey;
    private bool open = false;

    private void OnTriggerEnter(Collider other)
    {
        if (needKey && !GameManager.Instance.hasKey) return;
        if (pointsToOpen > GameManager.Instance.score) return;

        open = true;
    }

    private void Update()
    {
        if (!open) return;
        
        Vector3 dir = (destinationPosition.position - objectToMove.transform.position).normalized;
        objectToMove.transform.position += dir * speed;
    }
}
