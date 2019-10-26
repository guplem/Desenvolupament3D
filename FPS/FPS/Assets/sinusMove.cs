using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class sinusMove : MonoBehaviour
{
    
    
    [SerializeField] private float size = 1;
    [SerializeField] private Vector3 direction = Vector3.up;

    private void Update()
    {
        transform.position += size * Mathf.Sin(Time.timeSinceLevelLoad) * direction;
    }
}
