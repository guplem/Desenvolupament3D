using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerCamera;
    [SerializeField] private Portal m_MirrorPortal;
    [SerializeField] private Camera m_PortalCamera;
    [SerializeField] private float m_MinFOV=8.0f;
    [SerializeField] private float m_maxFov=60.0f;
    [SerializeField] private float m_MaxFOVDistance=20.0f;
    [SerializeField] private float m_NearClipOffset=0.5f;
    
    void Update ()
    {
        Vector3 l_ReflectedVector=m_PlayerCamera.position-transform.position;
        l_ReflectedVector=transform.position-l_ReflectedVector;
        Debug.DrawLine(transform.position , l_ReflectedVector, Color.red);
        
        l_ReflectedVector=transform.InverseTransformPoint(l_ReflectedVector);
        m_MirrorPortal.m_PortalCamera.transform.position = m_MirrorPortal.transform.TransformPoint(l_ReflectedVector);
        m_PortalCamera.transform.forward=(transform.position-m_PortalCamera.transform.position).normalized;
        m_PortalCamera.nearClipPlane = Vector3.Distance(m_PortalCamera.transform.position, this.transform.position)+m_NearClipOffset;
        
        //FOV
        Vector3 l_PlayerToPortal=transform.position-m_PlayerCamera.position;
        float l_Distance=l_PlayerToPortal.magnitude;
        float l_Pct=1.0f-Mathf.Min(l_Distance/m_MaxFOVDistance, 1.0f);
        m_MirrorPortal.m_PortalCamera.fieldOfView=Mathf.Lerp(m_MinFOV, m_maxFov, l_Pct);
    }
}
