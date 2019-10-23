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
        /*
        m_CurrentAmmoCount--;

        Ray l_CameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_CameraRay, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            GameObject inst = Instantiate(m_ShootHitParticles, l_RaycastHit.point, Quaternion.Euler(l_RaycastHit.normal));
            Destroy(inst, 10f);
            inst = Instantiate(decal, l_RaycastHit.point+(l_RaycastHit.normal*0.05f), Quaternion.LookRotation(l_RaycastHit.normal), l_RaycastHit.transform);
            GameManager.Instance.AddDecal(inst);

            Health hitHP = l_RaycastHit.transform.GetComponent<Health>();
            if (hitHP != null)
                hitHP.Hurt(weaponDamage);
        }

        timeForNextShoot = timeBetweenShoots;

        weaponAnimationComponent.CrossFade(shootWeapon.name, 0.1f);
        weaponAnimationComponent.CrossFadeQueued(idleWeapon.name);
        
        UpdateAmmoVisuals();
        */
    }

    private bool CanShoot()
    {
        return true;
    }


}