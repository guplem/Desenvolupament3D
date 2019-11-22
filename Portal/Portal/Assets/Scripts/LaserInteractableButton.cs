using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteractableButton : Interactable, LaserBehaviour
{
    private void Start()
    {
        interactableByUser = false;
        interactableByTrigger = false;
    }

    public void OnHitStart(Vector3 woldPosition)
    {
        OnStartInteract.Invoke();
    }

    public void OnHitEnd()
    {
        OnEndInteract.Invoke();
    }
    
    private void OnTriggerStay(Collider other)
    {
    }
    
    private void OnTriggerEnter(Collider other)
    {
    }
    
    private void OnTriggerExit(Collider other)
    {
    }
}
