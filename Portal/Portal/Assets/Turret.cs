using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private LineRenderer m_LineRenderer; 
    [SerializeField] private LayerMask m_CollisionLayerMask; 
    [SerializeField] private float m_MaxDistance=250.0f; 
    [SerializeField] private float m_AngleLaserActive=60.0f;
    [SerializeField] private Transform initialLaserPoint;
    private bool l_RayActive;

    private void Update()
    {
        //Laser basics
        Vector3 l_EndRaycastPosition=Vector3.forward*m_MaxDistance;
        RaycastHit l_RaycastHit;
        if(Physics.Raycast(new Ray(initialLaserPoint.position, m_LineRenderer.transform.forward), out l_RaycastHit, m_MaxDistance, m_CollisionLayerMask.value))
        {
            l_EndRaycastPosition=Vector3.forward*l_RaycastHit.distance;
        }
        m_LineRenderer.SetPosition(0, initialLaserPoint.position);
        m_LineRenderer.SetPosition(1, l_EndRaycastPosition);
        
        //Active laser or not
        float l_DotAngleLaserActive=Mathf.Cos(m_AngleLaserActive*Mathf.Deg2Rad*0.5f);
        l_RayActive=Vector3.Dot(transform.up, Vector3.up)>l_DotAngleLaserActive;
    }
}
