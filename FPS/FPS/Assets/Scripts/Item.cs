using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TItemType { LIFE, AMMO, SHIELD } 

    public TItemType m_ItemType;
    public int quantity; 


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        switch (m_ItemType)
        {
            case TItemType.LIFE:
                TakeLifeItem();
                break;
            case TItemType.AMMO:
                TakeAmmoItem();
                break;
            case TItemType.SHIELD:
                TakeShieldItem();
                break;
        }
    }

    void TakeLifeItem()
    {

        if (GameManager.Instance.player.health.ModifyLife(quantity) )
            DestroyItem();

    }

    void TakeAmmoItem()
    {
        if (GameManager.Instance.player.AddAmmo(quantity) )
            DestroyItem();
    }

    void TakeShieldItem()
    {
        if (GameManager.Instance.player.health.ModifyShield(quantity) )
            DestroyItem();
    }


    private void DestroyItem()
    {
        GameManager.Destroy(gameObject);
    }
}
