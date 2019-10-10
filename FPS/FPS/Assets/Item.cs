using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TItemType { LIFE, /*WEAPON,*/ AMMO } 
    //GameManager m_GameController;
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
        }
    }

    void TakeLifeItem()
    {
        if (GameManager.Instance.player.life < GameManager.Instance.player.maxLife)
        {
            GameManager.Instance.player.AddLife(quantity);
            DestroyItem();
        }
    }

    void TakeAmmoItem()
    {
        GameManager.Instance.player.AddAmmo(quantity);
        DestroyItem();
    }


    private void DestroyItem()
    {
        GameManager.Destroy(gameObject);
    }
}
