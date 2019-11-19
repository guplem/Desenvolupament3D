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
            
        TeleportToMirror(other.gameObject, 0.1f);
        m_MirrorPortal.IgnoreEnterOf(other.gameObject);

        
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
    
    
    float velocityFU = -1f;
    private Rigidbody rbFU = null;
    public void TeleportToMirror(GameObject otherGameObject, float offset)
    {
        
        
        try
        {
            rbFU = otherGameObject.GetComponent<Rigidbody>();
            velocityFU = rbFU.velocity.magnitude;
        } catch (Exception) { }
        
        otherGameObject.transform.position = GetOtherPortalPosition(this, offset);

        Vector3 direction = transform.InverseTransformDirection(-otherGameObject.transform.forward);


        try
        {
            Debug.Log("CHARACTER ROTATED");
            otherGameObject.GetComponent<RotationController>().m_Yaw = m_MirrorPortal.transform.rotation.eulerAngles.y;
        }
        catch (Exception)
        {
            otherGameObject.transform.forward = m_MirrorPortal.transform.TransformDirection(direction);
            Debug.Log("OBJECT ROTATED");
        }

        Debug.DrawRay(m_MirrorPortal.transform.position, otherGameObject.transform.forward, Color.red, 3f);
        
    }

    private void FixedUpdate()
    {
        if (rbFU != null && velocityFU > 0)
        {
            rbFU.velocity = rbFU.gameObject.transform.forward * velocityFU;
            rbFU = null;
            velocityFU = -1;
        }
    }

    private Vector3 GetOtherPortalPosition(Portal portal, float offset)
    {
        Vector3 otherForward = portal.m_MirrorPortal.transform.forward;
        Vector3 other = portal.m_MirrorPortal.transform.position;

        offset += .01f;

        Vector3 l_Position = portal.transform.InverseTransformPoint(transform.position);
        Vector3 returnPosition = portal.m_MirrorPortal.transform.TransformPoint(l_Position);

        if (!(Math.Abs(otherForward.z) < 0.01f))
            returnPosition.z = other.z + (otherForward.z * offset);
        else if (!(Math.Abs(otherForward.x) < 0.01f))
            returnPosition.x = other.x + (otherForward.x * offset);

        return returnPosition;
    }
    
}
