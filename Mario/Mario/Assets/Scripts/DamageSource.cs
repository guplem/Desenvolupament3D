using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    
    private bool m_isCausingDamage = false;
    public float m_DamageRepeatRate;
    public int m_DamageAmount;
    public bool Repeating = false;
   
    private void OnTriggerEnter(Collider other)
    {
        m_isCausingDamage = true;
        PlayerManager character = other.gameObject.GetComponent<PlayerManager>();

        if (character != null)
        {
            if (Repeating)
            {
                StartCoroutine(TakeDamage(character, m_DamageRepeatRate));
            }
            else
            {
                character.TakeDamage(m_DamageAmount);
            }
        }

    }

    IEnumerator TakeDamage(PlayerManager character, float repeatRate)
    {
        while (m_isCausingDamage)
        {
            character.TakeDamage(m_DamageAmount);
            
            yield return  new WaitForSeconds(repeatRate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerManager character = other.gameObject.GetComponent<PlayerManager>();
        if (character != null)
        {
            m_isCausingDamage = false;
        }
    }
    
    

    // Update is called once per frame
   

}