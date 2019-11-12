using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] public Camera playerCamera;
    [SerializeField] public GameObject portalPlaceHolder;

    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && CanShoot())
            Shoot();
            
        Vector3 playerCamPos = playerCamera.transform.position;
        Ray ray=new Ray(playerCamPos, transform.position-playerCamPos);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            portalPlaceHolder.transform.position = raycastHit.point;
            Debug.Log(raycastHit.collider.gameObject.name);            
        }
    }


    void Shoot()
    {
        //TODO: create portal
        Debug.Log("SHOOT");
    }

    private bool CanShoot()
    {
        // TODO: portal check
        return true;
    }


}