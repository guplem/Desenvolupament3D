using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;

    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && CanShoot())
            Shoot();
    }


    void Shoot()
    {

    }

    private bool CanShoot()
    {
        return true;
    }


}