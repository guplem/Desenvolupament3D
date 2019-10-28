using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonHitableZone : Health
{
    [SerializeField] private Enemy enemy;

    public override void Hurt(int damage)
    { }

    public override bool ModifyLife(int qtty)
    {
        if (qtty < 0)
            enemy.SetState(Enemy.State.Hit);
        return true;
    }

    public override bool ModifyShield(int qtty)
    {
        if (qtty < 0)
            enemy.SetState(Enemy.State.Hit);
        return true;
    }
    

    public override bool IsDead()
    { return enemy.health.IsDead(); }

    public override float GetHp()
    { return enemy.health.GetHp(); }

    public override float GetShield()
    { return enemy.health.GetShield(); }
}
