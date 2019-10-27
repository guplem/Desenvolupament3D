using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HitableZone : Health
{
    [SerializeField] private Health healthSystem;

    public override void Hurt(int damage)
    {
        healthSystem.Hurt(damage);
    }
    
    public override bool ModifyLife(int qtty)
    {
        return healthSystem.ModifyLife(qtty);
    }
    
    public override bool ModifyShield(int qtty)
    {
        return healthSystem.ModifyShield(qtty);
    }
    

    public override bool IsDead()
    {
        return healthSystem.IsDead();
    }

    public override float GetHp()
    {
        return healthSystem.GetHp();
    }

    public override float GetShield()
    {
        return healthSystem.GetShield();
    }

}
