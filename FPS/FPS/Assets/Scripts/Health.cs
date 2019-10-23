using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Health : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;

    [SerializeField] private float shield;
    [SerializeField] private float maxShield;
    
    [SerializeField] private TMP_Text lifeIndicatorText;
    [SerializeField] private TMP_Text shieldIndicatorText;

    [SerializeField] private UnityEvent onDeath;

    private float originalLife;
    private float originalShield;

    private void Awake()
    {
        originalLife = life;
        originalShield = shield;
    }

    private void OnEnable()
    {
        life = originalLife;
        shield = originalShield;
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public void Hurt(int damage)
    {
        for (int d = 0; d < damage; d++)
        {
            // If there is shield
            if (shield > 0)
            {
                int valRand = UnityEngine.Random.Range(0, 100);
                // 1/4 of the damage goes to the health
                if (valRand > 75)
                    ModifyLife(-1);
                
                // the rest of the damage goes to the shield
                else
                    ModifyShield(-1);
            }
            
            // If there is no shield
            else
            {
                //All the damage goes to the life
                ModifyLife(-1);
            }
        }
    }
    
    public bool ModifyLife(int qtty)
    {
        if (qtty > 0 && life >= maxLife) return false;
        if (qtty < 0 && life <= 0) return false;
        
        life += qtty;
        
        if (life > maxLife)
            life = maxLife;

        if (life <= 0)
        {
            onDeath.Invoke();
            life = 0;
        }
        
        UpdateVisuals();
        return true;

    }
    
    public bool ModifyShield(int qtty)
    {
        if (qtty > 0 && shield >= maxShield) return false;
        if (qtty < 0 && shield <= 0) return false;
        
        shield += qtty;

        if (shield > maxShield)
            shield = maxShield;
        
        if (shield <= 0)
            shield = 0;
        
        UpdateVisuals();
        return true;

    }
    
    
    private void UpdateVisuals()
    {
        if (lifeIndicatorText != null)
            lifeIndicatorText.text = "HP: " + life;
        
        
        if (shieldIndicatorText != null)
            shieldIndicatorText.text = ( (shield > 0) ? ("Shield: " + shield) : ("") );
    }

    public bool IsDead()
    {
        return life <= 0;
    }

    public float GetHp()
    {
        return life;
    }

    public float GetShield()
    {
        return shield;
    }
}
