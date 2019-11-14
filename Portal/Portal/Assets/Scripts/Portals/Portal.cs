using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Portal : PortalBases
{
    [SerializeField] private bool deactivateAtStart;
    
    private Transform m_PlayerCamera;
    [SerializeField] private Portal m_MirrorPortal;
    [SerializeField] private Camera m_PortalCamera;
    [SerializeField] private float m_NearClipOffset=0.5f;

    private List<GameObject> objectsToIgnoreAtTriggerEnter = new List<GameObject>();
    
    private void Start()
    {
        m_PlayerCamera = GameManager.Instance.player.playerCamera.transform;
        gameObject.SetActive(!deactivateAtStart);
        transform.localScale = new Vector3(defaultSize, defaultSize, defaultSize);
    }


    void Update ()
    {
        //CAMERA MOVEMENT
        Vector3 l_EulerAngles=transform.rotation.eulerAngles;
        Quaternion l_Rotation=Quaternion.Euler(l_EulerAngles.x, l_EulerAngles.y+180.0f, l_EulerAngles.z);
        Matrix4x4 l_WorldMatrix=Matrix4x4.TRS(transform.position, l_Rotation, transform.localScale);
        Vector3 l_ReflectedPosition=l_WorldMatrix.inverse.MultiplyPoint3x4(m_PlayerCamera.position);
        Vector3 l_ReflectedDirection=l_WorldMatrix.inverse.MultiplyVector(m_PlayerCamera.forward);
        m_MirrorPortal.m_PortalCamera.transform.position=m_MirrorPortal.transform.TransformPoint(l_ReflectedPosition);
        m_MirrorPortal.m_PortalCamera.transform.forward=m_MirrorPortal.transform.TransformDirection(l_ReflectedDirection);
        m_PortalCamera.nearClipPlane=Vector3.Distance(m_PortalCamera.transform.position, this.transform.position)+m_NearClipOffset;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (objectsToIgnoreAtTriggerEnter.Contains(other.gameObject))
            return;
        
        Rigidbody rb = other.GetComponent<Rigidbody>();
        CharacterController cc = other.GetComponent<CharacterController>();

        if (rb == null && cc == null) return;
        
        if (cc != null)
            cc.enabled = false;
            
        TeleportToMirror(other.gameObject);
        m_MirrorPortal.IgnoreEnterOf(other.gameObject);
        RotationController rc = other.GetComponent<RotationController>();
        if (rc != null)
            rc.m_Yaw = m_MirrorPortal.transform.rotation.eulerAngles.y -180;
        
        if (cc != null)
            cc.enabled = true;
        

    }
    
    private void IgnoreEnterOf(GameObject go)
    {
        objectsToIgnoreAtTriggerEnter.Add(go);
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsToIgnoreAtTriggerEnter.Contains(other.gameObject))
            objectsToIgnoreAtTriggerEnter.Remove(other.gameObject);
    }

    private void TeleportToMirror(GameObject otherGameObject)
    {
        Transform newTransform = otherGameObject.transform;
        
        Vector3 l_Position = transform.InverseTransformPoint(transform.position);
        newTransform.position = m_MirrorPortal.transform.TransformPoint(l_Position) + m_MirrorPortal.transform.forward*-0.1f;
        Vector3 l_Direction = transform.InverseTransformDirection(-transform.forward);
        newTransform.forward = m_MirrorPortal.transform.TransformDirection(l_Direction);
        newTransform.localScale = (m_MirrorPortal.GetProportionalSizeToDefault() / GetProportionalSizeToDefault() ) * newTransform.localScale.x * Vector3.one;
        //newTransform.rotation =  otherGameObject.transform.rotation * Quaternion.Inverse(m_MirrorPortal.transform.rotation) * transform.rotation;
        otherGameObject.transform.SetTransform(newTransform);

        Rigidbody rb = otherGameObject.GetComponent<Rigidbody>();
        if (rb == null) return;
        //TODO: set the rb.velocity to the proper value relative to the rotation and relative velocity.
    }

    public float GetProportionalSizeToDefault()
    {
        return transform.localScale.x / defaultSize;
    }
    
}
