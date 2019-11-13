using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool deactivateAtStart;
    
    /*[SerializeField] */private Transform m_PlayerCamera;
    [SerializeField] private Portal m_MirrorPortal;
    [SerializeField] private Camera m_PortalCamera;
    [SerializeField] private float m_NearClipOffset=0.5f;
    //FOV
    /*[SerializeField] private float m_MinFOV=8.0f;
    [SerializeField] private float m_maxFov=60.0f;
    [SerializeField] private float m_MaxFOVDistance=20.0f;*/

    
    private void Start()
    {
        m_PlayerCamera = GameManager.Instance.player.playerCamera.transform;
        gameObject.SetActive(!deactivateAtStart);
    }


    void Update ()
    {
        Vector3 l_EulerAngles=transform.rotation.eulerAngles;
        Quaternion l_Rotation=Quaternion.Euler(l_EulerAngles.x, l_EulerAngles.y+180.0f, l_EulerAngles.z);
        Matrix4x4 l_WorldMatrix=Matrix4x4.TRS(transform.position, l_Rotation, transform.localScale);
        Vector3 l_ReflectedPosition=l_WorldMatrix.inverse.MultiplyPoint3x4(m_PlayerCamera.position);
        Vector3 l_ReflectedDirection=l_WorldMatrix.inverse.MultiplyVector(m_PlayerCamera.forward);
        m_MirrorPortal.m_PortalCamera.transform.position=m_MirrorPortal.transform.TransformPoint(l_ReflectedPosition);
        m_MirrorPortal.m_PortalCamera.transform.forward=m_MirrorPortal.transform.TransformDirection(l_ReflectedDirection);
        m_PortalCamera.nearClipPlane=Vector3.Distance(m_PortalCamera.transform.position, this.transform.position)+m_NearClipOffset;
        
        //FOV
        /*Vector3 l_PlayerToPortal=transform.position-m_PlayerCamera.position;
        float l_Distance=l_PlayerToPortal.magnitude;
        float l_Pct=1.0f-Mathf.Min(l_Distance/m_MaxFOVDistance, 1.0f);
        m_MirrorPortal.m_PortalCamera.fieldOfView=Mathf.Lerp(m_MinFOV, m_maxFov, l_Pct);*/
    }
}
