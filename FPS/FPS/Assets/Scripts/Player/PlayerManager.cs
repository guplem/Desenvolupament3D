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
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject m_ShootHitParticles;
    [SerializeField] private GameObject decal;
    [SerializeField] private LayerMask m_ShootLayerMask;
    [SerializeField] private float timeBetweenShoots = 0.5f;
    private float timeForNextShoot = 0.2f;
    [SerializeField] private Animation weaponAnimationComponent;

    [SerializeField] private AnimationClip idleWeapon;
    [SerializeField] private AnimationClip shootWeapon;
    [SerializeField] private AnimationClip reloadWeapon;

    [SerializeField] private TMP_Text ammoGun;
    [SerializeField] private TMP_Text ammoCarry;
    [HideInInspector] public float life { get; private set; }
    [HideInInspector] public float maxLife;

    private void Start()
    {
        m_CurrentAmmoCarry = 100;
        weaponAnimationComponent.CrossFade(idleWeapon.name);
        UpdateAmmoVisuals();
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

    public void AddLife(int qtty)
    {
        life += qtty;
        UpdateHealthVisuals();
    }

    public void AddAmmo(int qtty)
    {
        m_CurrentAmmoCarry += qtty;
        UpdateAmmoVisuals();
    }
    
    private void UpdateHealthVisuals()
    {
        Debug.Log("UpdateHealthVisuals Not done yet");
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
        m_CurrentAmmoCount--;

        Ray l_CameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_CameraRay, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            GameObject inst = Instantiate(m_ShootHitParticles, l_RaycastHit.point, Quaternion.Euler(l_RaycastHit.normal));
            Destroy(inst, 10f);
            inst = Instantiate(decal, l_RaycastHit.point+(l_RaycastHit.normal*0.05f), Quaternion.LookRotation(l_RaycastHit.normal), l_RaycastHit.transform);
            GameManager.Instance.AddDecal(inst);
        }

        timeForNextShoot = timeBetweenShoots;

        weaponAnimationComponent.CrossFade(shootWeapon.name, 0.1f);
        weaponAnimationComponent.CrossFadeQueued(idleWeapon.name);
        
        UpdateAmmoVisuals();
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