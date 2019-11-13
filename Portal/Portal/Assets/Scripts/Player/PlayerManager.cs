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
    [SerializeField] public LayerMask layersWhereDisplayPortalPlaceHolder;

    private void Start()
    {
        GameObject go = gameObject;
        if (portalPlaceHolder == null)
            Debug.LogError("portalPlaceHolder is null in the " + this, go);

    }

    private void Update()
    {

        bool canBePlaced = false;
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            Transform playerCameraTransform = playerCamera.transform;
            RaycastHit raycastHit;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, 200, layersWhereDisplayPortalPlaceHolder))
            {
                // Set Physically
                portalPlaceHolder.ModifySize(Input.mouseScrollDelta.y);
                portalPlaceHolder.transform.position = raycastHit.point;
                portalPlaceHolder.transform.rotation = Quaternion.LookRotation(raycastHit.normal*-1);
                
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
        
    }




}