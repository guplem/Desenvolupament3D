using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TItemType { LIFE = 0, AMMO = 1, SHIELD = 2, KEY = 3 }


    public TItemType m_ItemType;
    public int quantity; 

    [SerializeField] private Material[] materials;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = materials[(int)m_ItemType];
    }

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
            
            case TItemType.KEY:
                GameManager.Instance.hasKey = true;
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
