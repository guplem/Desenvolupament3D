using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int m_CurrentAmmoCount = 20;
    [HideInInspector] public int m_CurrentAmmoCarry { get; private set; }
    int m_reloadAmmount = 25;
    [SerializeField] private int weaponDamage = 5;
    [SerializeField] public Camera playerCamera;
    [SerializeField] private GameObject m_ShootHitParticles;
    [SerializeField] private GameObject decal;
    [SerializeField] private AudioSource shootingSound;
    [SerializeField] private AudioSource hitMarkerSound;
    [SerializeField] private LayerMask m_ShootLayerMask;
    [SerializeField] private float timeBetweenShoots = 0.5f;
    private float timeForNextShoot = 0.2f;
    [SerializeField] private Animation weaponAnimationComponent;

    [SerializeField] private AnimationClip idleWeapon;
    [SerializeField] private AnimationClip shootWeapon;
    [SerializeField] private AnimationClip reloadWeapon;

    [SerializeField] private TMP_Text ammoGun;
    [SerializeField] private TMP_Text ammoCarry;
    public Health health { get; private set; }

    private void Start()
    {
        m_CurrentAmmoCarry = 100;
        weaponAnimationComponent.CrossFade(idleWeapon.name);
        UpdateAmmoVisuals();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (timeForNextShoot > 0)
            timeForNextShoot -= Time.deltaTime;

        if (Input.GetButton("Fire1") && CanShoot())
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }
    }



    public bool AddAmmo(int qtty)
    {
        m_CurrentAmmoCarry += qtty;
        UpdateAmmoVisuals();
        return true;
    }
    


    private void ReloadWeapon()
    {
        int reloadAmmount = m_reloadAmmount - m_CurrentAmmoCount;
        if (m_CurrentAmmoCarry < reloadAmmount)
            reloadAmmount = m_CurrentAmmoCarry;

        m_CurrentAmmoCount += reloadAmmount;
        m_CurrentAmmoCarry -= reloadAmmount;

        UpdateAmmoVisuals();

        if (reloadAmmount > 0)
        {
            weaponAnimationComponent.CrossFade(reloadWeapon.name);
            weaponAnimationComponent.CrossFadeQueued(idleWeapon.name);
        }
    }

    void Shoot()
    {
        //SHOOT
        m_CurrentAmmoCount--;
        shootingSound.Play();
        timeForNextShoot = timeBetweenShoots;
        weaponAnimationComponent.CrossFade(shootWeapon.name, 0.1f);
        weaponAnimationComponent.CrossFadeQueued(idleWeapon.name);
        UpdateAmmoVisuals();
        
        //HIT CONTROL
        Ray l_CameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_CameraRay, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            //Ensure proper col
            if (l_RaycastHit.collider.isTrigger) return;
                
            //Particles
            GameObject inst = Instantiate(m_ShootHitParticles, l_RaycastHit.point, Quaternion.Euler(l_RaycastHit.normal));
            Destroy(inst, 10f);
            
            //Decal
            inst = Instantiate(decal, l_RaycastHit.point+(l_RaycastHit.normal*0.05f), Quaternion.LookRotation(l_RaycastHit.normal), l_RaycastHit.transform);
            inst.transform.SetGlobalScale(decal.transform.localScale);
            GameManager.Instance.AddDecal(inst);

            // Damage
            Health hitHP = l_RaycastHit.transform.GetComponent<Health>();
            if (hitHP != null)
            {
                hitHP.Hurt(weaponDamage);
                hitMarkerSound.Play();
            }
                
        }


    }

    private bool CanShoot()
    {
        return m_CurrentAmmoCount > 0 && timeForNextShoot <= 0;
    }

    private void UpdateAmmoVisuals()
    {
        ammoCarry.text = m_CurrentAmmoCarry.ToString();
        ammoGun.text = m_CurrentAmmoCount.ToString();
    }
}