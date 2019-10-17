using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float maxLife;

    [SerializeField] private float shield;
    [SerializeField] private float maxShield;
    
    [SerializeField] private TMP_Text lifeIndicatorText;
    [SerializeField] private TMP_Text shieldIndicatorText;

    [SerializeField] private UnityEvent onDeath;

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
                // 1/4 of the damage goes to the health
                if (d % 4 == 0)
                    life -= 1;
                
                // the rest of the damage goes to the shield
                else
                    shield -= 1;
            }
            
            // If there is no shield
            else
            {
                //All the damage goes to the life
                life -= 1;
            }
        }
    }
    
    public bool ModifyLife(int qtty)
    {
        if (!(life < maxLife)) return false;
        
        life += qtty;
        
        if (life > maxLife)
            life = maxLife;
        
        if (life < 0)
            life = 0;
        
        UpdateVisuals();
        return true;

    }
    
    public bool ModifyShield(int qtty)
    {
        if (!(shield < maxShield)) return false;
        
        shield += qtty;

        if (shield > maxShield)
            shield = maxShield;
        
        if (shield < 0)
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
}
