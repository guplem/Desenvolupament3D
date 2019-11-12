using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] public Camera playerCamera;

    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && CanShoot())
            Shoot();
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