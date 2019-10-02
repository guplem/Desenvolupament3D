using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int m_CurrentAmmoCount=10;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject m_ShootHitParticles;
    [SerializeField] private LayerMask m_ShootLayerMask;
    [SerializeField] private float timeBetweenShoots = 0.5f;
    private float timeForNextShoot = 0.2f;
    
    private void Update()
    {
        if (timeForNextShoot > 0)
            timeForNextShoot -= Time.deltaTime;
        
        if(Input.GetButton("Fire1") && CanShoot())
            Shoot();
    }
    
    void Shoot()
    {
        m_CurrentAmmoCount--;
        
        Ray l_CameraRay=camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        if(Physics.Raycast(l_CameraRay, out l_RaycastHit, 200.0f, m_ShootLayerMask))
            Instantiate(m_ShootHitParticles, l_RaycastHit.point, Quaternion.Euler(l_RaycastHit.normal));

        timeForNextShoot = timeBetweenShoots;
    }

    private bool CanShoot()
    {
        return m_CurrentAmmoCount > 0 && timeForNextShoot <= 0;
    }
}
