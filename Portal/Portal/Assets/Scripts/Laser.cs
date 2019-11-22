using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 
public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer; 
    [SerializeField] private LayerMask layersToCollide; 
    private float maxLaserDistance=250.0f;
    private LaserBehaviour currentLaserBehaviour;
    
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //Draw Laser
        Vector3 endPosition=transform.forward*maxLaserDistance;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit raycastHit, maxLaserDistance, layersToCollide.value))
        {
            endPosition = raycastHit.point;
            
            LaserBehaviour newLaserBehaviour = raycastHit.collider.GetComponent<LaserBehaviour>();
            if (newLaserBehaviour != currentLaserBehaviour)
            {
                if (newLaserBehaviour != null)
                    newLaserBehaviour.OnHitStart(raycastHit.point);
                if (currentLaserBehaviour != null)
                    currentLaserBehaviour.OnHitEnd();
                currentLaserBehaviour = newLaserBehaviour;
            }
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void OnDestroy()
    {
        try
        {
            currentLaserBehaviour.OnHitEnd();
        }
        catch (Exception){};
    }
}
