using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserInterruptor : MonoBehaviour, LaserBehaviour
{
    public UnityEvent OnStartInteract;
    public UnityEvent OnEndInteract;

    public void OnHitStart(Vector3 woldPosition)
    {
        OnStartInteract.Invoke();
    }

    public void OnHitEnd()
    {
        OnEndInteract.Invoke();
    }
}