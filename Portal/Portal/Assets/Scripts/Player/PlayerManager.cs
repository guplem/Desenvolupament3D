using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class PortalController
{
    public enum PortalType
    {
        Blue,
        Orange
    }

}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public Camera playerCamera;
    [SerializeField] public PortalPlaceHolder portalPlaceHolder;
    [SerializeField] public LayerMask allExceptPlayer;

    private void Start()
    {
        GameObject go = gameObject;
        if (portalPlaceHolder == null)
            Debug.LogError("portalPlaceHolder is null in the " + this, go);

    }

    private void Update()
    {
        if (m_ObjectAttached != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_ObjectAttached.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * 8.5f;
                m_ObjectAttached = null;
            }
            
            if (Input.GetMouseButtonDown(1))
                m_ObjectAttached = null;

            UpdateAttachedObject();
        }
        else
        {
            bool canBePlaced = false;
            if (Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
            {
                Transform playerCameraTransform = playerCamera.transform;
                RaycastHit raycastHit;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, 200, allExceptPlayer))
                {
                    // Set Physically
                    portalPlaceHolder.ModifySize(Input.mouseScrollDelta.y*Time.deltaTime*2.5f);
                    portalPlaceHolder.transform.position = raycastHit.point;
                    portalPlaceHolder.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
                
                    // Set Visually
                    canBePlaced = portalPlaceHolder.CanSpawnPortal();
                    if (canBePlaced)
                        portalPlaceHolder.SetVisuals(Input.GetButton("Fire1") ? PortalController.PortalType.Blue : PortalController.PortalType.Orange);

                    // Creation
                    if (Input.GetButtonUp("Fire1") && canBePlaced)
                        GameManager.Instance.PlacePortal(PortalController.PortalType.Blue, portalPlaceHolder.transform);
        
                    else if (Input.GetButtonUp("Fire2") && canBePlaced)
                        GameManager.Instance.PlacePortal(PortalController.PortalType.Orange, portalPlaceHolder.transform);
                }
            }
            portalPlaceHolder.SetVisuals(canBePlaced);



            if (Input.GetMouseButtonDown(2))
            {
                Transform playerCameraTransform = playerCamera.transform;
                RaycastHit raycastHit;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, 10, allExceptPlayer))
                    if (raycastHit.collider.gameObject.CompareTag("Pickable"))
                    {
                        m_ObjectAttached = raycastHit.collider.gameObject;
                    }
            }
        }
    }

    private GameObject m_ObjectAttached
    {
        get { return _pickedObject; }
        set {
            if (value == null)
            {
                attachedObjectReachedPos = false;
                if (_pickedObject != null)
                {
                    _pickedObject = null;
                    m_RigidbodyOfObjectAttached.isKinematic=false;
                    //m_ObjectAttached.GetComponent<Companion>().SetTeleport(true);
                    //m_ObjectAttached.AddForce(m_AttachingPosition.forward*Force);
                }
            }
            else
            {
                m_RigidbodyOfObjectAttached = value.GetComponent<Rigidbody>();
                m_AttachingObjectStartRotation = value.transform.rotation;
                if (m_RigidbodyOfObjectAttached == null)
                    Debug.LogError(value.name + " does not have a RigidBody but hs the tag 'Pickable'.");
                m_RigidbodyOfObjectAttached.isKinematic=true;
                _pickedObject = value;
            }
            
        }
    }
    private GameObject _pickedObject = null;
    private Rigidbody m_RigidbodyOfObjectAttached;
    [SerializeField] private Transform m_AttachingPosition;
    [SerializeField] private float m_AttachingObjectSpeed;
    private bool attachedObjectReachedPos = false;
    private Quaternion m_AttachingObjectStartRotation;


    private void UpdateAttachedObject()
    {
        if (m_ObjectAttached == null)
            return;
        
        Vector3 l_EulerAngles=m_AttachingPosition.rotation.eulerAngles;
        if(!attachedObjectReachedPos)
        {
            Vector3 l_Direction=m_AttachingPosition.transform.position-m_ObjectAttached.transform.position;
            float l_Distance=l_Direction.magnitude;
            float l_Movement=m_AttachingObjectSpeed*Time.deltaTime;
            if(l_Movement>=l_Distance)
            {
                attachedObjectReachedPos=true;
                m_RigidbodyOfObjectAttached.MovePosition(m_AttachingPosition.position);
                m_RigidbodyOfObjectAttached.MoveRotation(Quaternion.Euler(0.0f, l_EulerAngles.y, l_EulerAngles.z));
            }
            else
            {
                l_Direction/=l_Distance;
                m_RigidbodyOfObjectAttached.MovePosition(m_ObjectAttached.transform.position+l_Direction*l_Movement);
                m_RigidbodyOfObjectAttached.MoveRotation(Quaternion.Lerp(m_AttachingObjectStartRotation, Quaternion.Euler(0.0f, l_EulerAngles.y, l_EulerAngles.z), 1.0f-Mathf.Min(l_Distance/1.5f, 1.0f)));
            }
        }
        else
        {
            m_RigidbodyOfObjectAttached.MoveRotation(Quaternion.Euler(0.0f, l_EulerAngles.y, l_EulerAngles.z));
            m_RigidbodyOfObjectAttached.MovePosition(m_AttachingPosition.position);
        }
    }


}