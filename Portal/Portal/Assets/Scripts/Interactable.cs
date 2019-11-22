using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnStartInteract;
    public UnityEvent OnEndInteract;

    [SerializeField] protected bool interactableByUser = true;
    [SerializeField] protected bool interactableByTrigger = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (!interactableByUser) 
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnStartInteract.Invoke();
            } 
            else if (Input.GetKeyUp(KeyCode.E))
            {
                OnEndInteract.Invoke();
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!interactableByTrigger) 
            return;
        
        OnStartInteract.Invoke();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!interactableByTrigger) 
            return;
        
        OnEndInteract.Invoke();
    }
}
