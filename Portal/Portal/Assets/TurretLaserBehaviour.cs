using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserBehaviour : MonoBehaviour, LaserBehaviour
{
    public void OnHitStart(Vector3 woldPosition)
    {
        Destroy(gameObject);
    }

    public void OnHitEnd()
    {
        
    }
}
